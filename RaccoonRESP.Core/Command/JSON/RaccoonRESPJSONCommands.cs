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
            // Implementation for appending a value to a JSON array at the end
            var commandResponse = await _client.SendCommandAsync("JSON.ARRAPPEND", key, path, value);
            return commandResponse;
        }
    }
}
