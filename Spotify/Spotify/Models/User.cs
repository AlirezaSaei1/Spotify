namespace Spotify.Models;

public class User
{
    public User(int userId, string name, string lastName, string email, string password)
    {
        UserId = userId;
        Name = name;
        LastName = lastName;
        Email = email;
        Password = password;
    }

    public int UserId { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
}