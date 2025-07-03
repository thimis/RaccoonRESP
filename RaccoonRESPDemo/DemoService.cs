using Microsoft.Extensions.Hosting;
using RaccoonRESPClient.Core;

namespace RaccoonRESPClientConsole
{
    public class DemoService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connectionSettings = new RaccoonRESPConnectionSettings() { Port = 23456 };
            var connection = new RaccoonRESPConnection(connectionSettings);

            var client = new RaccoonRESPClient.Core.RaccoonRESPClient(connection);
            //Connect to Redis Server
            await client.ConnectAsync();

            var commands = client.GetCommandUtility();

            var return0 = await client.SendCommandAsync($"AUTH passwordraccoon");

            var return1 = await commands.String.Set("TimeKey", "somevalue");

            var return2 = await commands.String.Get("TimeKey");
        }
    }
}