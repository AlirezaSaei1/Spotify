using System.ComponentModel.DataAnnotations;

namespace Spotify.Models;

public class User
{
    public User(int userId, string name, string lastName, string email, string password, List<Music> playlist)
    {
        UserId = userId;
        Name = name;
        LastName = lastName;
        Email = email;
        Password = password;
        Playlist = playlist;
    }

    [Key]
    public int UserId { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    //Relationships
    public List<Music> Playlist { get; set; }
    public List<ArtistUser>? Followings { get; set; }
    
}