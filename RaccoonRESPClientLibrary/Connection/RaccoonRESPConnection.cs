using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static RaccoonRESPClientLibrary.Model.RESPEnums;

namespace RaccoonRESPClientLibrary.Connection
{
    public class RaccoonRESPConnection
    {
        public readonly string _host;
        public readonly int _port;
        public readonly ProtocolVersion _protocol;

        internal TcpClient client;
        internal NetworkStream stream;
        internal StreamWriter writer;
        internal StreamReader reader;


        public RaccoonRESPConnection(string Host = "127.0.0.1", int Port = 6379, ProtocolVersion Protocol = ProtocolVersion.RESP3)
        {
            _host = Host;
            _port = Port;
            _protocol = Protocol;
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
