using Aspire.Hosting;
using RaccoonRESPClient.Core;
using System.Threading.Tasks;

namespace RaccoonRESP.CoreTest
{
    [TestFixture]
    public class StringCommandsIntegrationTest
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

            //Turn off random port assignment for testing purposes
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
        public async Task Test_Command_Set_IsSuccess()
        {
            var key = Guid.NewGuid().ToString();
            var value = Guid.NewGuid().ToString();
            var commands = _client.GetCommandUtility();

            var return1 = await commands.String.Set(key, value);

            Assert.That(return1.Response, Is.EqualTo("OK"));
        }

        [TestCase("Key","Value")]
        [TestCase("Key:159", "Value159")]
        [TestCase($"kEy:AppleTango", "Value563741Gamma")]
        [Test]
        public async Task Test_Command_Set_Get_IsStored(string key, string value)
        {
            var commands = _client.GetCommandUtility();

            var return1 = await commands.String.Set(key, value);
            var return2 = await commands.String.Get(key);

            Assert.That(return2.Response, Is.EqualTo(value));
        }

        [Test]
        public async Task Test_Command_Set_Get_IsStoredWithRandomGuids()
        {
            var commands = _client.GetCommandUtility();
            var key = Guid.NewGuid().ToString();
            var value = Guid.NewGuid().ToString();

            var return1 = await commands.String.Set(key, value);
            var return2 = await commands.String.Get(key);

            Assert.That(return2.Response, Is.EqualTo(value));
        }

        [Test]
        public async Task Test_Command_Set_Exist_IsTrue()
        {
            var commands = _client.GetCommandUtility();
            var key = Guid.NewGuid().ToString();
            var value = Guid.NewGuid().ToString();

            var return1 = await commands.String.Set(key, value);
            var return2 = await commands.String.Exist(key);

            Assert.That(return2.Response, Is.EqualTo(1));
        }

        [Test]
        [Repeat(10)]
        public async Task Test_Command_Set_Exist_IsTrueMultiKey([Random(1,100,1)] int NumberOfKeys)
        {
            string[] keys = GenerateRandomKeys(NumberOfKeys);
            var commands = _client.GetCommandUtility();

            foreach (var key in keys)
            {
                var value = Guid.NewGuid().ToString();
                var return1 = await commands.String.Set(key, value);
            }
            
            var return2 = await commands.String.Exist(keys);

            Assert.That(return2.Response, Is.EqualTo(NumberOfKeys));
        }

        [Test]
        public async Task Test_Command_Set_Append_Get_IsStored()
        {
            var commands = _client.GetCommandUtility();
            var key = Guid.NewGuid().ToString();
            var value = Guid.NewGuid().ToString();
            var appendValue = Guid.NewGuid().ToString();

            var return1 = await commands.String.Set(key, value);
            var return2 = await commands.String.Append(key,appendValue);
            var return3 = await commands.String.Get(key);

            Assert.That(return3.Response, Is.EqualTo(value + appendValue));
        }

        [Test]
        public async Task Test_Command_Increment_Get_IsStored()
        {
            var commands = _client.GetCommandUtility();
            var key = Guid.NewGuid().ToString();
            var incrementValue = 0;

            var return1 = await commands.String.Increment(key);
            incrementValue++;
            var return2 = await commands.String.Get(key);

            var result = int.Parse((string)return2.Response);
            Assert.That(result, Is.EqualTo(incrementValue));
        }

        [Test]
        public async Task Test_Command_Increment2_Decrement_Get_IsStored()
        {
            var commands = _client.GetCommandUtility();
            var key = Guid.NewGuid().ToString();
            var incrementValue = 0;

            var return1 = await commands.String.Increment(key);
            incrementValue++;
            var return2 = await commands.String.Increment(key);
            incrementValue++;
            var return3 = await commands.String.Decrement(key);
            incrementValue--;
            var return4 = await commands.String.Get(key);

            var result = int.Parse((string)return4.Response);
            Assert.That(result, Is.EqualTo(incrementValue));
        }

        [Test]
        public async Task Test_Command_IncrementBy_Get_IsStored()
        {
            var commands = _client.GetCommandUtility();
            var key = Guid.NewGuid().ToString();
            var incrementValue = 10;

            var return1 = await commands.String.IncrementBy(key, incrementValue);
            var return2 = await commands.String.Get(key);

            var result = int.Parse((string)return2.Response);
            Assert.That(result, Is.EqualTo(incrementValue));
        }

        [Test]
        public async Task Test_Command_IncrementBy_DecrementBy_Get_IsStored()
        {
            var commands = _client.GetCommandUtility();
            var key = Guid.NewGuid().ToString();
            var incrementValue = 10;
            var decrementValue = 5;

            var return1 = await commands.String.IncrementBy(key, incrementValue);
            var return2 = await commands.String.DecrementBy(key, decrementValue);
            var return3 = await commands.String.Get(key);

            var result = int.Parse((string)return3.Response);
            Assert.That(result, Is.EqualTo(incrementValue - decrementValue));
        }

        private string[] GenerateRandomKeys(int numberOfKeys)
        {
            var keys = new string[numberOfKeys];

            for (int i = 0; i < numberOfKeys; i++)
            {
                keys[i] = Guid.NewGuid().ToString();
            }
            return keys;
        }
    }
}
