using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RedisRaccoonClientDotNetConsole
{
    public class RedisResp3Client
    {
        private readonly string host;
        private readonly int port;

        private TcpClient client;
        private NetworkStream stream;
        private StreamWriter writer;
        private StreamReader reader;

        public RedisResp3Client(string host = "127.0.0.1", int port = 6379)
        {
            this.host = host;
            this.port = port;
        }

        public async Task ConnectAsync()
        {
            client = new TcpClient();
            await client.ConnectAsync(host, port);
            stream = client.GetStream();

            writer = new StreamWriter(stream, Encoding.ASCII) { AutoFlush = true };
            reader = new StreamReader(stream, Encoding.ASCII);
        }

        public async Task<object> SendCommandAsync(string command)
        {
            if (client == null || !client.Connected)
                throw new InvalidOperationException("Redis client is not connected.");

            // Send command in RESP3 array-of-bulk-strings format
            string[] parts = command.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var sb = new StringBuilder($"*{parts.Length}\r\n");
            foreach (var p in parts)
                sb.Append($"${Encoding.UTF8.GetByteCount(p)}\r\n{p}\r\n");

            await writer.WriteAsync(sb.ToString());
            return await ParseResp3Async();
        }

        private async Task<object> ParseResp3Async()
        {
            int prefix = reader.Read();
            if (prefix == -1) throw new IOException("Connection closed by server");

            switch ((char)prefix)
            {
                case '+': return await ReadSimpleStringAsync();
                //case '-': return new RedisError(await ReadLineAsync());
                //case ':': return long.Parse(await ReadLineAsync(), CultureInfo.InvariantCulture);
                //case '$': return await ReadBulkStringAsync();
                //case '"': return await ReadDoubleQuotedAsync();
                //case '`': return await ReadVerbatimAsync();
                //case '_': await ReadLineAsync(); return null;
                //case ',': return double.Parse(await ReadLineAsync(), CultureInfo.InvariantCulture);
                //case '#': return bool.Parse(await ReadLineAsync());
                //case '!': return await ReadBlobErrorAsync();
                //case '*': return await ReadArrayAsync();
                //case '%': return await ReadMapAsync();
                //case '~': return await ReadSetAsync();
                //case '>': return await ReadPushAsync();
                //case '|': return await ReadAttribAsync();
                default:
                    throw new NotSupportedException($"Unsupported RESP3 prefix '{(char)prefix}'");
            }
        }

        private Task<string> ReadSimpleStringAsync() => ReadLineAsync();
        private async Task<string> ReadLineAsync()
        {
            string? line = await reader.ReadLineAsync();
            if (line is null) throw new EndOfStreamException();
            return line;
        }

        private string FormatCommandAsRESP3(string input)
        {
            return $"{input}\r\n";
        }
    }
}
