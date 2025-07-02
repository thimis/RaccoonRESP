using RaccoonRESPClient.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaccoonRESPClient.Core
{
    public class StringCommands : IStringCommands
    {
        private RaccoonRESPClient _client;

        public StringCommands(RaccoonRESPClient client)
        {
            _client = client;
        }

        public async Task<RaccoonRESPResponse> Exist(string key)
        {
            var existsResponse = await _client.SendCommandAsync("EXISTS", key);
            return existsResponse;
        }
        public async Task<RaccoonRESPResponse> Set(string key, string value)
        {
            var setResponse = await _client.SendCommandAsync($"SET {key} {value}");
            return setResponse;
        }

        public async Task<RaccoonRESPResponse> Get(string key)
        {
            var getResponse = await _client.SendCommandAsync($"GET {key}");
            return getResponse;
        }
        public async void Append(string key, string value)
        {
            // Implementation for appending a string in the database
            var appendResponse = await _client.SendCommandAsync("APPEND", key, value);
        }
        public void Decrement(string key)
        {
            // Implementation for decrementing a string value in the database
            var decrementResponse = _client.SendCommandAsync($"DECR {key}").Result;
        }
        public void DecrementBy(string key, long decrement)
        {
            // Implementation for decrementing a string value by a specified amount in the database
            var decrementByResponse = _client.SendCommandAsync($"DECRBY {key} {decrement}").Result;
        }
        public string GetDelete(string key)
        {
            // Implementation for getting and deleting a string value in the database
            var getDeleteResponse = _client.SendCommandAsync($"GETDEL {key}").Result;
            return getDeleteResponse?.ToString() ?? string.Empty;
        }
        public string GetExpire(string key)
        {
            // Implementation for getting the expiration time of a string value in the database
            var getExpireResponse = _client.SendCommandAsync($"GETEX {key}").Result;
            return getExpireResponse?.ToString() ?? string.Empty;
        }
        public void Increment(string key)
        {
            // Implementation for incrementing a string value in the database
            var incrementResponse = _client.SendCommandAsync($"INCR {key}").Result;
        }
        public void IncrementBy(string key, long increment)
        {
            // Implementation for incrementing a string value by a specified amount in the database
            var incrementByResponse = _client.SendCommandAsync($"INCRBY {key} {increment}").Result;
        }
        public void IncrementByFloat(string key, double increment)
        {
            // Implementation for incrementing a string value by a float amount in the database
            var incrementByFloatResponse = _client.SendCommandAsync($"INCRBYFLOAT {key} {increment}").Result;
        }
        public long LongestCommonSubstring(string key1, string key2)
        {
            // Implementation for finding the longest common substring between two string values in the database
            var lcsResponse = _client.SendCommandAsync($"LCS {key1} {key2}").Result;
            return long.TryParse(lcsResponse?.ToString(), out long result) ? result : 0;
        }
        public List<string> MGet(params string[] keys)
        {
            // Implementation for getting multiple string values in the database
            var keysString = string.Join(" ", keys);
            var mgetResponse = _client.SendCommandAsync($"MGET {keysString}").Result;
            return mgetResponse is IEnumerable<string> responseList ? responseList.ToList() : new List<string>();
        }
        public void MSet(Dictionary<string, string> keyValuePairs)
        {
            // Implementation for setting multiple string values in the database
            var pairs = keyValuePairs.Select(kvp => $"{kvp.Key} {kvp.Value}");
            var msetResponse = _client.SendCommandAsync($"MSET {string.Join(" ", pairs)}").Result;
        }
        public void MSetNx(Dictionary<string, string> keyValuePairs)
        {
            // Implementation for setting multiple string values in the database if they do not exist
            var pairs = keyValuePairs.Select(kvp => $"{kvp.Key} {kvp.Value}");
            var msetNxResponse = _client.SendCommandAsync($"MSETNX {string.Join(" ", pairs)}").Result;
        }
        public void PSetEx(string key, long milliseconds, string value)
        {
            // Implementation for setting a string value with a specified expiration time in milliseconds
            var psetExResponse = _client.SendCommandAsync($"PSETEX {key} {milliseconds} {value}").Result;
        }
        public void SetEx(string key, long seconds, string value)
        {
            // Implementation for setting a string value with a specified expiration time in seconds
            var setExResponse = _client.SendCommandAsync($"SETEX {key} {seconds} {value}").Result;
        }
        public void SetNx(string key, string value)
        {
            // Implementation for setting a string value if it does not exist
            var setNxResponse = _client.SendCommandAsync($"SETNX {key} {value}").Result;
        }
        public void SetRange(string key, long offset, string value)
        {
            // Implementation for setting a substring of a string value in the database
            var setRangeResponse = _client.SendCommandAsync($"SETRANGE {key} {offset} {value}").Result;
        }
        public long StringLength(string key)
        {
            // Implementation for getting the length of a string value in the database
            var strLenResponse = _client.SendCommandAsync($"STRLEN {key}").Result;
            return long.TryParse(strLenResponse?.ToString(), out long result) ? result : 0;
        }
        public string Substring(string key, long start, long end)
        {
            // Implementation for getting a substring of a string value in the database
            var substringResponse = _client.SendCommandAsync($"SUBSTR {key} {start} {end}").Result;
            return substringResponse?.ToString() ?? string.Empty;
        }
    }
}
