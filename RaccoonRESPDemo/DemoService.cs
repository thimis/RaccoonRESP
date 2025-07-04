using Microsoft.Extensions.Hosting;
using RaccoonRESP.Core;

namespace RaccoonRESPDemo
{
    public class DemoService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //var connectionSettings = new RaccoonRESPConnectionSettings() { Port = 23456 , Password = "passwordraccoon" };
            //var connection = new RaccoonRESPConnection(connectionSettings);

            //var client = new RaccoonRESPClient.Core.RaccoonRESPClient(connection);
            ////Connect to Redis Server
            //await client.ConnectAsync();

            //var commands = client.GetCommandUtility();            

            //var return1 = await commands.String.Set("TimeKey", "somevalue");

            //var return2 = await commands.String.Get("TimeKey");
        }
    }
}