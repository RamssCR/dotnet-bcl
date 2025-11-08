using System.Text.Json;
using ModernNet.ObjectSamples;
using ModernNet.Libraries.IO;

const string path = "config.json";

JsonSerializerOptions options = new()
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = true,
    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault,
};

JsonFile<User> file = new(path, options);
var user = await file.LoadAsync();

if (string.IsNullOrWhiteSpace(user.Name))
{
    Console.WriteLine($"No user found, fill the following fields {Environment.NewLine}");

    Console.WriteLine("Enter an username:");
    var username = Console.ReadLine() ?? string.Empty;

    Console.WriteLine("Enter your age");
    int.TryParse(Console.ReadLine(), out var age);

    Console.WriteLine("Enter your email");
    var email = Console.ReadLine() ?? string.Empty;
    
    user = new User(username, age, email);
    await file.SaveAsync(user);
}
else
{
    Console.WriteLine("User information:");
    Console.WriteLine("Name: {0}", user.Name);
    Console.WriteLine("Age: {0}", user.Age);
    Console.WriteLine("Email: {0}", user.Email);
}