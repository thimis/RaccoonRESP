using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaccoonRESP.Core.Command.Bitmap
{
    public class RaccoonRESPBitmapCommands : IRaccoonRESPBitmapCommands
    {
        private RaccoonRESPClient _client;
        public RaccoonRESPBitmapCommands(RaccoonRESPClient client)
        {
            _client = client;
        }
        public async Task<RaccoonRESPResponse> BitCount(string key)
        {
            var commandResponse = await _client.SendCommandAsync("BITCOUNT", key);
            return commandResponse;
        }

        public async Task<RaccoonRESPResponse> BitCount(string key, long start, long end)
        {
            var commandResponse = await _client.SendCommandAsync("BITCOUNT", key, start, end);
            return commandResponse;
        }
        public async Task<RaccoonRESPResponse> BitCount(string key, long start, long end, BitCountMode mode)
        {
            if (mode == BitCountMode.BYTE)
            {
                var commandResponse = await _client.SendCommandAsync("BITCOUNT", key, start, end, "BYTE");
                return commandResponse;
            }
            else {
                var commandResponse = await _client.SendCommandAsync("BITCOUNT", key, start, end, "BIT");
                return commandResponse;
            }                       
        }
    }
    public enum BitCountMode
    {
        BYTE,
        BIT
    }
}
