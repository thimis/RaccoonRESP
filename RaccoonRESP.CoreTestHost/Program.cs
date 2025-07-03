using Aspire.Hosting;
using System.Xml.Linq;

var builder = DistributedApplication.CreateBuilder(args);

//TODO: Read password from environment variable or configuration
//var passwordResource = builder.AddParameter("cache-password", Environment.GetEnvironmentVariable("REDIS_PASSWORD"));

var passwordResource = builder.AddParameter("cache-password", "testpassword");

var cache = builder.AddRedis("testcache", port: 23456, password: passwordResource);

builder.Build().Run();
