using Aspire.Hosting;
using RaccoonRESPClient.Core;
using System.Threading.Tasks;

namespace RaccoonRESP.CoreTest
{
    [TestFixture]
    public class IntegrationTest1
    {
        private DistributedApplication _app;
        private RaccoonRESPClient.Core.RaccoonRESPClient _client;

        [OneTimeSetUp]
        public async Task SetUp()
        {
            var appHost = DistributedApplicationTestingBuilder.CreateAsync<Projects.RaccoonRESP_CoreTestHost>().Result;
            appHost.Services.ConfigureHttpClientDefaults(clientBuilder =>
            {
                clientBuilder.AddStandardResilienceHandler();
            });

            appHost.Configuration["DcpPublisher:RandomizePorts"] = "false";

            var app = appHost.BuildAsync().Result;
            var resourceNotificationService = app.Services.GetRequiredService<ResourceNotificationService>();
            await app.StartAsync();

            await resourceNotificationService.WaitForResourceAsync("testcache", KnownResourceStates.Running).WaitAsync(TimeSpan.FromSeconds(30));

            var connectionSettings = new RaccoonRESPConnectionSettings() { Port = 23456, Password = "testpassword" };
            var connection = new RaccoonRESPConnection(connectionSettings);

            var client = new RaccoonRESPClient.Core.RaccoonRESPClient(connection);
            //Connect to Redis Server
            await client.ConnectAsync();

            _app = app;
            _client = client;
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            _app?.Dispose();
        }

        [Test]
        public async Task FirstRaccoonRESPRedisTest()
        {
            var commands = _client.GetCommandUtility();
            var key = Guid.NewGuid().ToString();
            var value = Guid.NewGuid().ToString();

            var return1 = await commands.String.Set(key, value);
            var return2 = await commands.String.Get(key);

            Assert.That(return1.Response, Is.EqualTo("OK"));
            Assert.That(return2.Response, Is.EqualTo(value));
        }
    }
}
