


namespace RaccoonRESPClient.Core
{
    public interface IRaccoonRESPStringCommands
    {
        Task<RaccoonRESPResponse> Append(string key, string value);
        Task<RaccoonRESPResponse> Decrement(string key);
        Task<RaccoonRESPResponse> DecrementBy(string key, long decrement);
        Task<RaccoonRESPResponse> Exist(string key);
        Task<RaccoonRESPResponse> Get(string key);
        Task<RaccoonRESPResponse> GetDelete(string key);
        Task<RaccoonRESPResponse> GetExpire(string key);
        Task<RaccoonRESPResponse> Increment(string key);
        Task<RaccoonRESPResponse> IncrementBy(string key, long increment);
        Task<RaccoonRESPResponse> IncrementByFloat(string key, double increment);
        Task<RaccoonRESPResponse> LongestCommonSubstring(string key1, string key2);
        Task<RaccoonRESPResponse> MGet(params string[] keys);
        Task<RaccoonRESPResponse> MSet(Dictionary<string, string> keyValuePairs);
        Task<RaccoonRESPResponse> MSetNx(Dictionary<string, string> keyValuePairs);
        Task<RaccoonRESPResponse> PSetEx(string key, long milliseconds, string value);
        Task<RaccoonRESPResponse> Set(string key, string value);
        Task<RaccoonRESPResponse> SetEx(string key, long seconds, string value);
        Task<RaccoonRESPResponse> SetNx(string key, string value);
        Task<RaccoonRESPResponse> SetRange(string key, long offset, string value);
        Task<RaccoonRESPResponse> StringLength(string key);
        Task<RaccoonRESPResponse> Substring(string key, long start, long end);
    }
}