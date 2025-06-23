using RaccoonRESPClientLibrary.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaccoonRESPClientTest
{
    [TestFixture]
    internal class RaccoonRESPConnectionTest
    {
        //private PrimeService _primeService;

        [SetUp]
        public void SetUp()
        {
            //_primeService = new PrimeService();
        }

        [Test]
        public void RaccoonRESPConnection_Sets_Host_Default()
        {
            var connection = new RaccoonRESPConnection();

            var result = connection._host == "127.0.0.1";

            Assert.That(result, Is.True, "Default connection should be 127.0.0.1");
        }

        [Test]
        public void RaccoonRESPConnection_Sets_Host_Custom()
        {
            var customHost = "172.16.254.147";
            var connection = new RaccoonRESPConnection(Host: customHost);

            var result = connection._host == customHost;

            Assert.That(result, Is.True, $"Custom host connection should be {customHost}");
        }

        [Test]
        public void RaccoonRESPConnection_Sets_Post_Default()
        {
            var connection = new RaccoonRESPConnection();

            var result = connection._port == 6379;

            Assert.That(result, Is.True, "Default port should be 6379");
        }

        [Test]
        public void RaccoonRESPConnection_Sets_Port_Custom()
        {
            var customPort = 777;
            var connection = new RaccoonRESPConnection(Port: customPort);

            var result = connection._port == customPort;

            Assert.That(result, Is.True, $"Custom port connection should be {customPort}");
        }
    }
}
