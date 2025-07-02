
using RaccoonRESPClient.Core;

namespace RaccoonRESPClientLibrary.Database
{
    public class DatabaseString
    {
        private readonly RaccoonRESPClient.Core.RaccoonRESPClient _client;
        public DatabaseString(RaccoonRESPClient.Core.RaccoonRESPClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client), "Client cannot be null.");
        }
    }
}