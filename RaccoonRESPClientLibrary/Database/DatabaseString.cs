using RaccoonRESPClientLibrary.Client;

namespace RaccoonRESPClientLibrary.Database
{
    public class DatabaseString
    {
        private readonly RaccoonRESPClient _client;
        public DatabaseString(RaccoonRESPClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client), "Client cannot be null.");
        }
        public void Set(string key, string value)
        {
            // Implementation for setting a string in the database
            var setResponse = _client.SendCommandAsync($"SET {key} {value}").Result;
        }

        public string Get(string key)
        {
            var getResponse = _client.SendCommandAsync($"GET {key}").Result;
            return getResponse?.ToString() ?? string.Empty;
        }
    }
}