using System.Text.Json;
using ModernNet.Libraries.Text;

namespace ModernNet.Libraries.IO;

public class JsonFile<T>(string filePath, JsonSerializerOptions? options = null)
    where T : class, new()
{
    private readonly string _path = filePath;
    private readonly JsonSerializerOptions? _options = options ?? new JsonSerializerOptions()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault,
    };

    public async Task<T> LoadAsync()
    {
        var json = await JsonHelper.DeserializeAsync<T>(_path, _options);
        return json ?? new T();
    }
    
    public async Task SaveAsync(T content) =>
        await JsonHelper.SerializeAsync(_path, content,  _options);
}