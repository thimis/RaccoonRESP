using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace RaccoonRESPClient.Core
{
    public class RaccoonRESPClient : IRaccoonRESPClient
    {
        private readonly RaccoonRESPConnection _connection;        

        public RaccoonRESPClient(RaccoonRESPConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection), "Connection cannot be null.");
        }
        public RaccoonRESPCommands GetCommandUtility()
        {
            return new RaccoonRESPCommands
            {
                Client = this,
                String = new RaccoonRESPStringCommands(this),
                Transaction = new RaccoonRESPTransactionCommands(this)
            };
        }

        public async Task ConnectAsync()
        {
            if (_connection.client == null || !_connection.client.Connected)
            {
                await _connection.ConnectAsync();

                if (_connection.Password is not null)
                {
                    await AuthenticateAsync();
                }

                await HandshakeAsync();
            }
        }

        private async Task<RaccoonRESPResponse> AuthenticateAsync()
        {
            if (_connection.Name is null)
            {
                return await SendCommandAsync($"AUTH default {_connection.Password}");
            }
            else
            {
                return await SendCommandAsync($"AUTH {_connection.Name} {_connection.Password}");
            }
        }

        private async Task<RaccoonRESPResponse> HandshakeAsync()
        {
            if (_connection.Name is null)
            {
                var guid = Guid.NewGuid();

                if (_connection._protocol == RaccoonRESPEnums.ProtocolVersion.RESP3)
                {
                    return await SendCommandAsync($"HELLO 3 SETNAME {guid}");
                }
                if (_connection._protocol == RaccoonRESPEnums.ProtocolVersion.RESP2)
                {
                    return await SendCommandAsync($"HELLO 2 SETNAME {guid}");
                }
            }
            else
            {
                if (_connection._protocol == RaccoonRESPEnums.ProtocolVersion.RESP3)
                {
                    return await SendCommandAsync($"HELLO 3 SETNAME {_connection.Name}");
                }
                if (_connection._protocol == RaccoonRESPEnums.ProtocolVersion.RESP2)
                {
                    return await SendCommandAsync($"HELLO 2 SETNAME {_connection.Name}");
                }
            }

            throw new Exception("Handshake failed");
        }

        public async Task<RaccoonRESPResponse> SendCommandAsync(string command)
        {
            if (_connection.client == null || !_connection.client.Connected)
                throw new InvalidOperationException("Redis client is not connected.");

            // Send command in RESP3 array-of-bulk-strings format
            string[] parts = command.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var sb = new StringBuilder($"*{parts.Length}\r\n");
            foreach (var p in parts)
                sb.Append($"${Encoding.ASCII.GetByteCount(p)}\r\n{p}\r\n");

            await _connection.writer.WriteAsync(sb.ToString());
            return new RaccoonRESPResponse() { Response = await ParseResp3Async() };
        }

        public async Task<RaccoonRESPResponse> SendCommandAsync(string command, string key, string value)
        {
            if (_connection.client == null || !_connection.client.Connected)
                throw new InvalidOperationException("Redis client is not connected.");

            // Send command in RESP3 array-of-bulk-strings format
            string[] parts = new[] { command, key, value };
            var sb = new StringBuilder($"*{parts.Length}\r\n");
            foreach (var p in parts)
                sb.Append($"${Encoding.ASCII.GetByteCount(p)}\r\n{p}\r\n");

            await _connection.writer.WriteAsync(sb.ToString());
            return new RaccoonRESPResponse() { Response = await ParseResp3Async() };
        }

        public async Task<RaccoonRESPResponse> SendCommandAsync(string command, string key)
        {
            if (_connection.client == null || !_connection.client.Connected)
                throw new InvalidOperationException("Redis client is not connected.");

            // Send command in RESP3 array-of-bulk-strings format
            string[] parts = new[] { command, key };
            var sb = new StringBuilder($"*{parts.Length}\r\n");
            foreach (var p in parts)
                sb.Append($"${Encoding.ASCII.GetByteCount(p)}\r\n{p}\r\n");

            await _connection.writer.WriteAsync(sb.ToString());
            return new RaccoonRESPResponse() { Response = await ParseResp3Async() };
        }

        private async Task<object> ParseResp3Async()
        {
            int prefix = _connection.reader.Read();
            if (prefix == -1) throw new IOException("Connection closed by server");

            switch ((char)prefix)
            {
                case '+': return await ReadSimpleStringAsync();
                case '-': return new RedisError(await ReadLineAsync());
                case ':': return long.Parse(await ReadLineAsync(), CultureInfo.InvariantCulture);
                case '$': return await ReadBulkStringAsync();
                case '"': return await ReadDoubleQuotedAsync();
                case '`': return await ReadVerbatimAsync();
                case '_': await ReadLineAsync(); return null;
                case ',': return double.Parse(await ReadLineAsync(), CultureInfo.InvariantCulture);
                case '#': return bool.Parse(await ReadLineAsync());
                case '!': return await ReadBlobErrorAsync();
                case '*': return await ReadArrayAsync();
                case '%': return await ReadMapAsync();
                case '~': return await ReadSetAsync();
                case '>': return await ReadPushAsync();
                case '|': return await ReadAttribAsync();
                default:
                    throw new NotSupportedException($"Unsupported RESP3 prefix '{(char)prefix}'");
            }
        }

        private Task<string> ReadSimpleStringAsync() => ReadLineAsync();
        private async Task<string> ReadLineAsync()
        {
            string? line = await _connection.reader.ReadLineAsync();
            if (line is null) throw new EndOfStreamException();
            return line;
        }

        private async Task<object?> ReadBulkStringAsync()
        {
            int len = int.Parse(await ReadLineAsync(), CultureInfo.InvariantCulture);
            if (len < 0) return null;

            char[] buf = new char[len];
            int read = await _connection.reader.ReadAsync(buf, 0, len);
            if (read != len) throw new IOException("Unexpected end of bulk string");
            await _connection.reader.ReadLineAsync();

            return new string(buf);
        }

        private async Task<string> ReadDoubleQuotedAsync()
            => (await ReadLineAsync()).UnescapeQuotedString();

        private async Task<VerbatimString> ReadVerbatimAsync()
        {
            string line = await ReadLineAsync();
            var parts = line.Split(':', 2);
            return new VerbatimString(parts[0], parts.Length > 1 ? parts[1] : "");
        }

        private async Task<RedisError> ReadBlobErrorAsync()
        {
            string line = await ReadLineAsync();
            var parts = line.Split(':', 2);
            return new RedisError(parts[1], parts[0]);
        }

        private async Task<List<object>> ReadArrayAsync()
        {
            int count = int.Parse(await ReadLineAsync(), CultureInfo.InvariantCulture);
            if (count < 0) return null!;

            var list = new List<object>(count);
            for (int i = 0; i < count; i++) list.Add(await ParseResp3Async());
            return list;
        }

        private async Task<Dictionary<object, object>> ReadMapAsync()
        {
            int count = int.Parse(await ReadLineAsync(), CultureInfo.InvariantCulture);
            if (count < 0) return null!;

            var map = new Dictionary<object, object>(count);
            for (int i = 0; i < count; i++)
            {
                var key = await ParseResp3Async();
                var val = await ParseResp3Async();
                map[key!] = val!;
            }
            return map;
        }

        private async Task<HashSet<object>> ReadSetAsync()
        {
            int count = int.Parse(await ReadLineAsync(), CultureInfo.InvariantCulture);
            if (count < 0) return null!;

            var set = new HashSet<object>();
            for (int i = 0; i < count; i++) set.Add(await ParseResp3Async());
            return set;
        }

        private async Task<KeyValuePair<string, List<object>>> ReadPushAsync()
        {
            var list = await ReadArrayAsync();
            return new KeyValuePair<string, List<object>>(list[0]?.ToString() ?? "", list);
        }

        private async Task<Dictionary<string, object>> ReadAttribAsync()
        {
            var attribs = await ReadMapAsync();
            var payload = await ParseResp3Async();
            return new Dictionary<string, object>
            {
                ["attrib"] = attribs!,
                ["value"] = payload!
            };
        }

        public class VerbatimString
        {
            public string Format { get; }
            public string Text { get; }
            public VerbatimString(string f, string t) => (Format, Text) = (f, t);
            public override string ToString() => $"Verbatim({Format}): {Text}";
        }

        public class RedisError
        {
            public string Message { get; }
            public string? Code { get; }
            public RedisError(string message, string code = null) =>
                (Message, Code) = (message, code);
            public override string ToString() =>
                Code is null ? $"Error: {Message}" : $"Error[{Code}]: {Message}";
        }
    }

    public static class StringEscapeExtensions
    {
        public static string UnescapeQuotedString(this string input)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '\\' && i + 1 < input.Length)
                {
                    i++;
                    sb.Append(input[i] switch
                    {
                        'n' => '\n',
                        'r' => '\r',
                        't' => '\t',
                        '"' => '\"',
                        '\\' => '\\',
                        _ => input[i]
                    });
                }
                else sb.Append(input[i]);
            }
            return sb.ToString();
        }
    }

}
