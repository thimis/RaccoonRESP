
namespace RaccoonRESP.Core
{
    public interface IRaccoonRESPJSONCommands
    {
        Task<RaccoonRESPResponse> ArrayAppendEnd(string key, string path, string value);
        Task<RaccoonRESPResponse> ArrayIndex(string key, string path, string value);
        Task<RaccoonRESPResponse> ArrayInsert(string key, string path, int index, string value);
        Task<RaccoonRESPResponse> ArrayLength(string key, string path);
        Task<RaccoonRESPResponse> ArrayPop(string key, string path, int index = -1);
        Task<RaccoonRESPResponse> ArrayTrim(string key, string path, int start, int stop);
        Task<RaccoonRESPResponse> Clear(string key, string path);
        Task<RaccoonRESPResponse> DebugMemory(string key, string path);
        Task<RaccoonRESPResponse> Delete(string key, string path);
        Task<RaccoonRESPResponse> Forget(string key, string path);
        Task<RaccoonRESPResponse> Get(string key, string path = ".");
        Task<RaccoonRESPResponse> Merge(string key, string path, string json);
        Task<RaccoonRESPResponse> MGet(string key, string path);
        Task<RaccoonRESPResponse> MSet(string key, string path, string json);
        Task<RaccoonRESPResponse> NumberIncrementBy(string key, string path, string increment);
        Task<RaccoonRESPResponse> NumberMultiplyBy(string key, string path, string multiplier);
        Task<RaccoonRESPResponse> ObjectKeys(string key, string path);
        Task<RaccoonRESPResponse> ObjectLength(string key, string path);
        Task<RaccoonRESPResponse> RespJson(string key, string path);
        Task<RaccoonRESPResponse> Set(string key, string path, string value, string NX, string XX);
        Task<RaccoonRESPResponse> StringAppend(string key, string value, string path);
        Task<RaccoonRESPResponse> StringLength(string key, string path);
        Task<RaccoonRESPResponse> Toggle(string key, string path);
        Task<RaccoonRESPResponse> Type(string key, string path);
    }
}