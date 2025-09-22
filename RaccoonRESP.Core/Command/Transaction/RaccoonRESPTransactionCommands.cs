using RaccoonRESP.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaccoonRESP.Core
{
    public class RaccoonRESPTransactionCommands : IRaccoonRESPTransactionCommands
    {
        private RaccoonRESPClient _client;

        public RaccoonRESPTransactionCommands(RaccoonRESPClient client)
        {
            _client = client;
        }

        public async Task<RaccoonRESPResponse> StartTranscation()
        {
            var commandResponse = await _client.SendCommandAsync("MULTI");
            return commandResponse;
        }
        public async Task<RaccoonRESPResponse> ExecuteTranscation()
        {
            var commandResponse = await _client.SendCommandAsync("EXEC");
            return commandResponse;
        }
        public async Task<RaccoonRESPResponse> DiscardTranscation()
        {
            var commandResponse = await _client.SendCommandAsync("DISCARD");
            return commandResponse;
        }
        public async Task<RaccoonRESPResponse> Watch(string key)
        {
            var commandResponse = await _client.SendCommandAsync("WATCH", key);
            return commandResponse;
        }
        public async Task<RaccoonRESPResponse> Unwatch()
        {
            var commandResponse = await _client.SendCommandAsync("UNWATCH");
            return commandResponse;
        }
    }
}
