using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RaccoonRESPClient.Core
{
    public class RaccoonRESPCommands
    {
        public RaccoonRESPClient Client { get; set; }
        public StringCommands String { get; set; }

        #region Transcation Commands
        public async Task<RaccoonRESPResponse> StartTranscation()
        {
            // Implementation for starting a transaction in the database
            var commandResponse = await Client.SendCommandAsync("MULTI");
            return commandResponse;
        }
        public async Task<RaccoonRESPResponse> ExecuteTranscation()
        {
            // Implementation for starting a transaction in the database
            var commandResponse = await Client.SendCommandAsync("EXEC");
            return commandResponse;
        }
        public async Task<RaccoonRESPResponse> DiscardTranscation()
        {
            // Implementation for discarding a transaction in the database
            var commandResponse = await Client.SendCommandAsync("DISCARD");
            return commandResponse;
        }
        public async Task<RaccoonRESPResponse> Watch(string key)
        {
            // Implementation for watching a key in the database
            var commandResponse = await Client.SendCommandAsync("WATCH", key);
            return commandResponse;
        }
        public async Task<RaccoonRESPResponse> Unwatch()
        {
            // Implementation for unwatching keys in the database
            var commandResponse = await Client.SendCommandAsync("UNWATCH");
            return commandResponse;
        }
        #endregion
    }

}
