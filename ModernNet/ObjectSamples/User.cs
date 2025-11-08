namespace ModernNet.ObjectSamples;

public class User
{
    public string Name { get; init; } = string.Empty;
    public int Age { get; init; }
    public string Email { get; init; } = string.Empty;
    
    public User() {}
    public User(string name, int age, string email) =>
        (Name, Age, Email) = (name, age, email);
}