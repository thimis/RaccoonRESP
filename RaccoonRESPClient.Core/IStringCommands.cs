

namespace RaccoonRESPClient.Core
{
    public interface IStringCommands
    {
        void Append(string key, string value);
        void Decrement(string key);
        void DecrementBy(string key, long decrement);
        Task<RaccoonRESPResponse> Exist(string key);
        Task<RaccoonRESPResponse> Get(string key);
        string GetDelete(string key);
        string GetExpire(string key);
        void Increment(string key);
        void IncrementBy(string key, long increment);
        void IncrementByFloat(string key, double increment);
        long LongestCommonSubstring(string key1, string key2);
        List<string> MGet(params string[] keys);
        void MSet(Dictionary<string, string> keyValuePairs);
        void MSetNx(Dictionary<string, string> keyValuePairs);
        void PSetEx(string key, long milliseconds, string value);
        Task<RaccoonRESPResponse> Set(string key, string value);
        void SetEx(string key, long seconds, string value);
        void SetNx(string key, string value);
        void SetRange(string key, long offset, string value);
        long StringLength(string key);
        string Substring(string key, long start, long end);
    }
}