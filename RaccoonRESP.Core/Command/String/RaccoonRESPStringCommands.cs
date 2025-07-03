using RaccoonRESPClient.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaccoonRESPClient.Core
{
    public class RaccoonRESPStringCommands : IRaccoonRESPStringCommands
    {
        private RaccoonRESPClient _client;

        public RaccoonRESPStringCommands(RaccoonRESPClient client)
        {
            _client = client;
        }

        public async Task<RaccoonRESPResponse> Exist(string key)
        {
            var response = await _client.SendCommandAsync("EXISTS", key);
            return response;
        }
        public async Task<RaccoonRESPResponse> Exist(string[] keys)
        {
            var response = await _client.SendCommandAsync("EXISTS", keys);
            return response;
        }
        public async Task<RaccoonRESPResponse> Set(string key, string value)
        {
            var response = await _client.SendCommandAsync($"SET {key} {value}");
            return response;
        }

        public async Task<RaccoonRESPResponse> Get(string key)
        {
            var response = await _client.SendCommandAsync($"GET {key}");
            return response;
        }
        public async Task<RaccoonRESPResponse> Append(string key, string value)
        {
            var response = await _client.SendCommandAsync("APPEND", key, value);
            return response;
        }
        public async Task<RaccoonRESPResponse> Decrement(string key)
        {
            var response = await _client.SendCommandAsync($"DECR {key}");
            return response;
        }
        public async Task<RaccoonRESPResponse> DecrementBy(string key, long decrement)
        {
            var response = await _client.SendCommandAsync($"DECRBY {key} {decrement}");
            return response;
        }
        public async Task<RaccoonRESPResponse> GetDelete(string key)
        {
            var response = await _client.SendCommandAsync($"GETDEL {key}");
            return response;
        }
        public async Task<RaccoonRESPResponse> GetExpire(string key)
        {
            var response = await _client.SendCommandAsync($"GETEX {key}");
            return response;
        }
        public async Task<RaccoonRESPResponse> Increment(string key)
        {
            var response = await _client.SendCommandAsync($"INCR {key}");
            return response;
        }
        public async Task<RaccoonRESPResponse> IncrementBy(string key, long increment)
        {
            var response = await _client.SendCommandAsync($"INCRBY {key} {increment}");
            return response;
        }
        public async Task<RaccoonRESPResponse> IncrementByFloat(string key, double increment)
        {
            var response = await _client.SendCommandAsync($"INCRBYFLOAT {key} {increment}");
            return response;
        }
        public async Task<RaccoonRESPResponse> LongestCommonSubstring(string key1, string key2)
        {
            var response = await _client.SendCommandAsync($"LCS {key1} {key2}");
            return response;
        }
        public async Task<RaccoonRESPResponse> MGet(params string[] keys)
        {
            var keysString = string.Join(" ", keys);
            var response = await _client.SendCommandAsync($"MGET {keysString}");
            return response;
        }
        public async Task<RaccoonRESPResponse> MSet(Dictionary<string, string> keyValuePairs)
        {
            var pairs = keyValuePairs.Select(kvp => $"{kvp.Key} {kvp.Value}");
            var response = await _client.SendCommandAsync($"MSET {string.Join(" ", pairs)}");
            return response;
        }
        public async Task<RaccoonRESPResponse> MSetNx(Dictionary<string, string> keyValuePairs)
        {
            var pairs = keyValuePairs.Select(kvp => $"{kvp.Key} {kvp.Value}");
            var response = await _client.SendCommandAsync($"MSETNX {string.Join(" ", pairs)}");
            return response;
        }
        public async Task<RaccoonRESPResponse> PSetEx(string key, long milliseconds, string value)
        {
            var response = await _client.SendCommandAsync($"PSETEX {key} {milliseconds} {value}");
            return response;
        }
        public async Task<RaccoonRESPResponse> SetEx(string key, long seconds, string value)
        {
            var response = await _client.SendCommandAsync($"SETEX {key} {seconds} {value}");
            return response;
        }
        public async Task<RaccoonRESPResponse> SetNx(string key, string value)
        {
            var response = await _client.SendCommandAsync($"SETNX {key} {value}");
            return response;
        }
        public async Task<RaccoonRESPResponse> SetRange(string key, long offset, string value)
        {
            var response = await _client.SendCommandAsync($"SETRANGE {key} {offset} {value}");
            return response;
        }
        public async Task<RaccoonRESPResponse> StringLength(string key)
        {
            var response = await _client.SendCommandAsync($"STRLEN {key}");
            return response;
        }
        public async Task<RaccoonRESPResponse> Substring(string key, long start, long end)
        {
            var response = await _client.SendCommandAsync($"SUBSTR {key} {start} {end}");
            return response;
        }
    }
}
