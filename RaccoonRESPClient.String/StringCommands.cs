using RaccoonRESPClient.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaccoonRESPClient.String
{
    public class StringCommands : IStringCommands
    {
        private RaccoonRESPClient.Core.RaccoonRESPClient _client;

        public StringCommands(RaccoonRESPClient.Core.RaccoonRESPClient client)
        {
            _client = client;
        }

        public async Task<object> Set(string key, string value) =>
            await _client.SendCommandAsync($"SET {key} {value}");

        public async Task<object> Get(string key) =>
            await _client.SendCommandAsync($"GET {key}");
    }
}
