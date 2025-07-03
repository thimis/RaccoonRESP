using RaccoonRESPClient.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaccoonRESPClient.Core
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
            // Implementation for starting a transaction in the database
            var commandResponse = await _client.SendCommandAsync("MULTI");
            return commandResponse;
        }
        public async Task<RaccoonRESPResponse> ExecuteTranscation()
        {
            // Implementation for starting a transaction in the database
            var commandResponse = await _client.SendCommandAsync("EXEC");
            return commandResponse;
        }
        public async Task<RaccoonRESPResponse> DiscardTranscation()
        {
            // Implementation for discarding a transaction in the database
            var commandResponse = await _client.SendCommandAsync("DISCARD");
            return commandResponse;
        }
        public async Task<RaccoonRESPResponse> Watch(string key)
        {
            // Implementation for watching a key in the database
            var commandResponse = await _client.SendCommandAsync("WATCH", key);
            return commandResponse;
        }
        public async Task<RaccoonRESPResponse> Unwatch()
        {
            // Implementation for unwatching keys in the database
            var commandResponse = await _client.SendCommandAsync("UNWATCH");
            return commandResponse;
        }
    }
}
