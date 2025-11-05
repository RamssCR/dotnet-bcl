using System.Collections;

namespace ModernNet.Libraries.Environment;
using System;

/*
 * System.Environment is a static class that gives you access to
 * 1. OS information.
 * 2. Environment Variables.
 * 3. System Directories.
 * 4. Runtime Settings (version, processors, etc...)
 * 5. Arguments passed to the program.
 * 
 * Everything is accessed directly. No instantiation needed.
 */

public static class Exercises
{
    public static void DisplayMachineName() =>
        Console.WriteLine($"Machine name: {Environment.MachineName}");

    public static void DisplayOsAndArchitecture()
    {
        Console.WriteLine($"OS Version: {Environment.OSVersion}");
        Console.WriteLine($"64-bit OS: {Environment.Is64BitOperatingSystem}");
        Console.WriteLine($"Processors: {Environment.ProcessorCount}");
    }

    public static void CurrentUser()
    {
        Console.WriteLine($"Username: {Environment.UserName}");
        Console.WriteLine($"User Domain: {Environment.UserDomainName}");
    }

    public static void DisplayImportantDirectories()
    {
        Console.WriteLine($"Current Directory: {Environment.CurrentDirectory}");
        Console.WriteLine($"System Directory: {Environment.SystemDirectory}");
        Console.WriteLine($"Temp Folder: {Environment.GetEnvironmentVariable("TEMP")}");
    }

    public static IEnumerable<DictionaryEntry> EnvironmentVariables(int amount) =>
        Environment.GetEnvironmentVariables()
            .Cast<DictionaryEntry>()
            .Take(amount);

    public static void DisplayRuntimeVersion() =>
        Console.WriteLine($".NET Version: {Environment.Version}");
}