namespace StreamPlay.Module.Users.Domain;

public class User
{
    public Guid Id { get; private set; }
    public string Username { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }

    private User(string username, string email, string password)
    {
        Id = Guid.NewGuid();
        Username = username;
        Email = email;
        Password = password;
    }

    public static User Create(string username, string email, string password)
    {
        return new(username, email, password);
    }
}