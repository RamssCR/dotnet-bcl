namespace ModernNet.Libraries.Text;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

public static class TextHelper
{
    /// <summary>
    /// Converts a string into bytes with UTF8 encoding.
    /// </summary>
    /// <param name="str">The string to convert.</param>
    /// <returns>The conversion to bytes.</returns>
    public static byte[] ConvertToBytes(string str) =>
        Encoding.UTF8.GetBytes(str);
    
    /// <summary>
    /// Converts an array of bytes into a string with UTF8 encoding.
    /// </summary>
    /// <param name="bytes">The bytes to convert.</param>
    /// <returns>The conversion to string.</returns>
    public static string ConvertToString(byte[] bytes) =>
        Encoding.UTF8.GetString(bytes);

    /// <summary>
    /// Creates an UTF8 encoded file through a stream writer.
    /// </summary>
    /// <param name="path">The location to create/overwrite the file</param>
    /// <param name="content">The content to append into the file.</param>
    /// <param name="overwrite">If the found file can be overwritten</param>
    /// <exception cref="IOException">If a file exists but overwrite option is set to false.</exception>
    public static async Task CreateEncodedFile(string path, string content, bool overwrite = true)
    {
        
        if (!overwrite && File.Exists(path))
            throw new IOException($"File '{path}' already exists");
        
        await using StreamWriter sw = new(path, false, Encoding.UTF8);
        await sw.WriteAsync(content);
    }

    /// <summary>
    /// Reads an UTF8 encoded file through a stream reader.
    /// </summary>
    /// <param name="path">The file to read.</param>
    public static async Task ReadEncodedFile(string path)
    {
        using StreamReader sr = new(path, Encoding.UTF8);

        while (await sr.ReadLineAsync() is { } line)
            Console.WriteLine(line);
    }

    /// <summary>
    /// Builds a string using the StringBuilder class.
    /// </summary>
    /// <param name="values">An enumerable set of strings.</param>
    /// <returns>A formatted string value.</returns>
    public static string BuildString(IEnumerable<string> values)
    {
        ArgumentNullException.ThrowIfNull(values);
        
        StringBuilder sb = new();
        return sb.AppendJoin(Environment.NewLine, values).ToString();
    }
}