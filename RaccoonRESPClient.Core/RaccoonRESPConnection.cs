using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static RaccoonRESPClient.Core.RaccoonRESPEnums;

namespace RaccoonRESPClient.Core
{
    public class RaccoonRESPConnection
    {
        public readonly string _host;
        public readonly int _port;
        public readonly ProtocolVersion _protocol;
        public readonly string Name;

        internal TcpClient client;
        internal NetworkStream stream;
        internal StreamWriter writer;
        internal StreamReader reader;


        public RaccoonRESPConnection(RaccoonRESPConnectionSettings connectionSettings)
        {
            _host = connectionSettings.Host;
            _port = connectionSettings.Port;
            _protocol = connectionSettings.Protocol;
            Name = connectionSettings.Name;
        }

        internal async Task ConnectAsync()
        {
            client = new TcpClient();
            await client.ConnectAsync(_host, _port);
            stream = client.GetStream();

            writer = new StreamWriter(stream, Encoding.ASCII) { AutoFlush = true };
            reader = new StreamReader(stream, Encoding.ASCII);
        }
    }
}
