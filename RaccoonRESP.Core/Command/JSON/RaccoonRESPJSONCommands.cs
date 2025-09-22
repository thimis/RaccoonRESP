using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaccoonRESP.Core
{
    public class RaccoonRESPJSONCommands : IRaccoonRESPJSONCommands
    {
        private RaccoonRESPClient _client;

        public RaccoonRESPJSONCommands(RaccoonRESPClient client)
        {
            _client = client;
        }

        public async Task<RaccoonRESPResponse> ArrayAppendEnd(string key, string path, string value)
        {
            var commandResponse = await _client.SendCommandAsync("JSON.ARRAPPEND", key, path, value);
            return commandResponse;
        }
        public async Task<RaccoonRESPResponse> ArrayIndex(string key, string path, string value)
        {
            var commandResponse = await _client.SendCommandAsync("JSON.ARRINDEX", key, path, value);
            return commandResponse;
        }
        public async Task<RaccoonRESPResponse> ArrayInsert(string key, string path, int index, string value)
        {
            var commandResponse = await _client.SendCommandAsync("JSON.ARRINSERT", key, path, index, value);
            return commandResponse;
        }
        public async Task<RaccoonRESPResponse> ArrayLength(string key, string path)
        {
            var commandResponse = await _client.SendCommandAsync("JSON.ARRLEN", key, path);
            return commandResponse;
        }
        public async Task<RaccoonRESPResponse> ArrayPop(string key, string path, int index = -1)
        {            
            var commandResponse = await _client.SendCommandAsync("JSON.ARRPOP", key, path, index);
            return commandResponse;
        }
        public async Task<RaccoonRESPResponse> ArrayTrim(string key, string path, int start, int stop)
        {
            var commandResponse = await _client.SendCommandAsync("JSON.ARRTRIM", key, path, start, stop);
            return commandResponse;
        }
        public async Task<RaccoonRESPResponse> Clear(string key, string path)
        {
            var commandResponse = await _client.SendCommandAsync("JSON.CLEAR", key, path);
            return commandResponse;
        }
        public async Task<RaccoonRESPResponse> DebugMemory(string key, string path)
        {
            var commandResponse = await _client.SendCommandAsync("JSON.DEBUGMEMORY", key, path);
            return commandResponse;
        }
        public async Task<RaccoonRESPResponse> Delete(string key, string path)
        {
            var commandResponse = await _client.SendCommandAsync("JSON.DEL", key, path);
            return commandResponse;
        }
        public async Task<RaccoonRESPResponse> Forget(string key, string path)
        {
            var commandResponse = await _client.SendCommandAsync("JSON.FORGET", key, path);
            return commandResponse;
        }
        public async Task<RaccoonRESPResponse> Get(string key, string path = ".")
        {
            var commandResponse = await _client.SendCommandAsync("JSON.GET", key, path);
            return commandResponse;
        }
        public async Task<RaccoonRESPResponse> Merge(string key, string path, string json)
        {
            var commandResponse = await _client.SendCommandAsync("JSON.MERGE", key, path, json);
            return commandResponse;
        }
        public async Task<RaccoonRESPResponse> MGet(string key, string path)
        {
            var commandResponse = await _client.SendCommandAsync("JSON.MGET", key, path);
            return commandResponse;
        }
        public async Task<RaccoonRESPResponse> MSet(string key, string path, string json)
        {
            var commandResponse = await _client.SendCommandAsync("JSON.MSET", key, path, json);
            return commandResponse;
        }
        public async Task<RaccoonRESPResponse> NumberIncrementBy(string key, string path, string increment)
        {
            var commandResponse = await _client.SendCommandAsync("JSON.NUMINCRBY", key, path, increment);
            return commandResponse;
        }
        [Obsolete("As of JSON version 2.0, this command is regarded as deprecated.")]
        public async Task<RaccoonRESPResponse> NumberMultiplyBy(string key, string path, string multiplier)
        {
            var commandResponse = await _client.SendCommandAsync("JSON.NUMLENBY", key, path, multiplier);
            return commandResponse;
        }
        public async Task<RaccoonRESPResponse> ObjectKeys(string key, string path)
        {
            var commandResponse = await _client.SendCommandAsync("JSON.OBJKEYS", key, path);
            return commandResponse;
        }
        public async Task<RaccoonRESPResponse> ObjectLength(string key, string path)
        {
            var commandResponse = await _client.SendCommandAsync("JSON.OBJLEN", key, path);
            return commandResponse;
        }
        [Obsolete("As of JSON version 2.6, this command is regarded as deprecated.")]
        public async Task<RaccoonRESPResponse> RespJson(string key, string path)
        {
            var commandResponse = await _client.SendCommandAsync("JSON.RESP", key, path);
            return commandResponse;
        }
        public async Task<RaccoonRESPResponse> Set(string key, string path, string value, string NX, string XX)
        {
            var commandResponse = await _client.SendCommandAsync("JSON.SET", key, path, value, NX, XX);
            return commandResponse;
        }
        public async Task<RaccoonRESPResponse> StringAppend(string key, string value, string path)
        {
            var commandResponse = await _client.SendCommandAsync("JSON.STRAPPEND", key, value, path);
            return commandResponse;
        }
        public async Task<RaccoonRESPResponse> StringLength(string key, string path)
        {
            var commandResponse = await _client.SendCommandAsync("JSON.STRLEN", key, path);
            return commandResponse;
        }
        public async Task<RaccoonRESPResponse> Toggle(string key, string path)
        {
            var commandResponse = await _client.SendCommandAsync("JSON.TOGGLE", key, path);
            return commandResponse;
        }
        public async Task<RaccoonRESPResponse> Type(string key, string path)
        {
            var commandResponse = await _client.SendCommandAsync("JSON.TYPE", key, path);
            return commandResponse;
        }
    }
}
