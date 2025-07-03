using Aspire.Hosting;
using System.Xml.Linq;

var builder = DistributedApplication.CreateBuilder(args);

//TODO: Read password from environment variable or configuration
//var passwordResource = builder.AddParameter("cache-password", Environment.GetEnvironmentVariable("REDIS_PASSWORD"));

var passwordResource = builder.AddParameter("cache-password", "passwordraccoon");

var cache = builder.AddRedis("cache", port: 23456, password: passwordResource);


builder.AddProject<Projects.RaccoonRESPDemo>("RaccoonRESPDemo").WithReference(cache);

//builder.AddProject("RaccoonRESPDeno", "C:\\Users\\Zero\\source\\repos\\RaccoonRESPClient\\RaccoonRESPClientConsole\\RaccoonRESPDemo.csproj")
//       .WithReference(cache);

builder.Build().Run();
