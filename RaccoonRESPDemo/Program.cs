using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RaccoonRESPDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);            
            builder.Services.AddHostedService<DemoService>();

            IHost host = builder.Build();
            host.Run();            
        }
    }
}
