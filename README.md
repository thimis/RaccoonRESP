# RaccoonRESP � C# Redis RESP Client Library

# Overview

RaccoonRESP is a lightweight and efficient C# library for interacting with Redis using the Redis Serialization Protocol (RESP). It enables .NET developers to connect to a Redis server and execute commands directly, making it easy to build high-performance caching and messaging functionality into .NET applications.

This library supports a wide range of Redis features and data types out of the box. Developers can perform common operations such as setting and retrieving string values, incrementing or decrementing counters, and executing Redis transactions (MULTI/EXEC) with ease.

# Getting Started

1. Install the Library

```dotnet add package RaccoonRESP.Core```

Add the RaccoonRESP.Core package to your .NET project. ple, using the .NET CLI:

(If the package is not yet on NuGet, you can clone the repository and reference the RaccoonRESP.Core project directly.)

2. Create a Client

Use the provided example to configure a connection, create a RaccoonRESPClient, set and get a string:

```csharp
using RaccoonRESP.Core;

// Configure connection settings to Redis, defaults to (localhost:6379)
var settings = new RaccoonRESPConnectionSettings();
var connection = new RaccoonRESPConnection(settings);
var client = new RaccoonRESPClient(connection);

// Connect to the Redis server
await client.ConnectAsync();

// Use the Command Utility to execute commands. Or you can use the client directly with SendCommandAsync.
var commands = client.GetCommandUtility();

// Execute Redis commands
await commands.String.Set("greeting", "Hello Redis!");
var value = await commands.String.Get("greeting");

Console.WriteLine($"Value for 'greeting': {value}");
// Output: Value for 'greeting': Hello Redis!
```
The example demonstrates using the Command Utility String command group to set and get a key�s value. The library provides methods for other Redis command groups.

# License

MIT LicenseRaccoonRESP is licensed under the MIT License, allowing commercial use and modification with proper attribution.

