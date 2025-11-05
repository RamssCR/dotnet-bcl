namespace ModernNet.Libraries.IO;
using System;
using System.IO;
using System.Threading.Tasks;

/*
 * System.IO gives you access to File Management and operations like:
 * 1. Creating Files/Directories.
 * 2. Reading Files (synchronously & asynchronously).
 * 3. Creating & Reading files through streams.
 * 4. Copying & Moving files/directories.
 * 5. Creating safe paths to files/directories.
 * 6. Removing files/directories.
 */

public static class FileSystemHelper
{
    public static string BasePath { get; set; } = Environment.CurrentDirectory;
    
    /// <summary>
    /// Gets all listed files.
    /// </summary>
    /// <returns>A list of directories.</returns>
    public static string[] ListFiles() =>
        Directory.GetFiles(Environment.CurrentDirectory);

    /// <summary>
    /// Reads a file asynchronously.
    /// </summary>
    /// <param name="name">The file to read.</param>
    /// <returns>The file content. null if no file is found.</returns>
    public static async Task<string?> ReadFileAsync(string name)
    {
        var directory = Dirname(name);
        return File.Exists(directory)
            ? await File.ReadAllTextAsync(directory)
            : null;
    }

    /// <summary>
    /// Creates a file asynchronously.
    /// </summary>
    /// <param name="name">The name the file will receive.</param>
    /// <param name="content">The content the file will contain.</param>
    /// <param name="overwrite">If the method can overwrite a file.</param>
    /// <exception cref="IOException">If the file already exists (overwrite set to false)</exception>
    public static async Task WriteFileAsync(string name, string content, bool overwrite = true)
    {
        var directory = Dirname(name);
        if (!overwrite && File.Exists(directory))
            throw new IOException($"File {name} already exists");
        
        await File.WriteAllTextAsync(directory, content);
    }
    
    /// <summary>
    /// Deletes a file.
    /// </summary>
    /// <param name="name">The name to search the file to delete with.</param>
    public static void DeleteFile(string name) =>
        File.Delete(Dirname(name));

    /// <summary>
    /// Copies a file to a specified destination.
    /// </summary>
    /// <param name="source">The source the file is located at.</param>
    /// <param name="destination">The destination the file will be copied to.</param>
    public static async Task CopyFileAsync(string source, string destination)
    {
        const int bufferSize = 81920;
        await using FileStream sourceStream = new(source, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, useAsync: true);
        await using FileStream destinationStream = new(destination, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize, useAsync: true);
        
        await sourceStream.CopyToAsync(destinationStream);
    }
    
    /// <summary>
    /// Creates a safe path in the given directory.
    /// </summary>
    /// <param name="directory">The directory to combine with the current path.</param>
    /// <returns>A string representing the entire directory path.</returns>
    private static string Dirname(string directory) =>
        Path.GetFullPath(Path.Combine(BasePath, directory));
}