


namespace RaccoonRESP.Core
{
    public interface IRaccoonRESPTransactionCommands
    {
        Task<RaccoonRESPResponse> DiscardTranscation();
        Task<RaccoonRESPResponse> ExecuteTranscation();
        Task<RaccoonRESPResponse> StartTranscation();
        Task<RaccoonRESPResponse> Unwatch();
        Task<RaccoonRESPResponse> Watch(string key);
    }
}