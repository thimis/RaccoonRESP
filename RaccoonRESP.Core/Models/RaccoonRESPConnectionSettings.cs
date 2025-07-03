using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RaccoonRESPClient.Core.RaccoonRESPEnums;

namespace RaccoonRESPClient.Core
{
    public class RaccoonRESPConnectionSettings
    {
        //string Host = "127.0.0.1", int Port = 6379, ProtocolVersion Protocol = ProtocolVersion.RESP3, string name = "RaccoonRESPClient"
        public string Host { get; set; } = "127.0.0.1";
        public int Port { get; set; } = 6379;
        public ProtocolVersion Protocol { get; set; } = ProtocolVersion.RESP3;
        public string Name { get; set; } = $"RaccoonRESPClient{new Guid()}";
    }
}
