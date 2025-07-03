using Aspire.Hosting;
using System.Xml.Linq;

var builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<ParameterResource> passwordResourceBuilder = builder.AddParameter("cache-password", "passwordraccoon");
var cache = builder.AddRedis("cache", port: 23456,password: passwordResourceBuilder);

builder.AddProject("RaccoonRESPDeno", "C:\\Users\\Zero\\source\\repos\\RaccoonRESPClient\\RaccoonRESPClientConsole\\RaccoonRESPDemo.csproj")
       .WithReference(cache);

builder.Build().Run();
