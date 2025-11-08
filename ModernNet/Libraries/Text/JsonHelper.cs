namespace ModernNet.Libraries.Text;
using System.IO;
using System.Text.Json;

public static class JsonHelper
{
    /// <summary>
    /// Serializes a C# object into a JSON format.
    /// </summary>
    /// <param name="path">The file location.</param>
    /// <param name="content">The content to append into the file.</param>
    /// <param name="options">Serializer options for serialization.</param>
    /// <typeparam name="T">The object type to serialize.</typeparam>
    public static async Task SerializeAsync<T>(string path, T content, JsonSerializerOptions? options = null)
    {
        var directory = Path.GetDirectoryName(path);
        
        if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
            Directory.CreateDirectory(directory);
        
        await using var stream = File.Create(path);
        await JsonSerializer.SerializeAsync(stream, content, options);
    }

    /// <summary>
    /// Deserializes a JSON file into the given object type to parse into.
    /// </summary>
    /// <param name="path">The file location.</param>
    /// <param name="options">Serializer options for deserialization.</param>
    /// <typeparam name="T">The object type the deserialized JSON is going to be parsed to.</typeparam>
    /// <returns>The object entirely deserialized.</returns>
    public static async Task<T?> DeserializeAsync<T>(string path, JsonSerializerOptions? options = null)
    {
        if (!File.Exists(path))
            return default;

        try
        {
            await using var stream = File.OpenRead(path);
            return await JsonSerializer.DeserializeAsync<T>(stream, options);
        }
        catch (JsonException)
        {
            Console.WriteLine($"[ERROR] Invalid JSON Format at {path}");
            return default;
        }
        catch (IOException ex)
        {
            Console.WriteLine($"[ERROR] File IO Issue: {ex.Message}");
            return default;
        }
    }
}