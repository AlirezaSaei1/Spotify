namespace Spotify.Core.Models;

public class Artist
{
    public int Id { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    
    public ICollection<Song> Songs { get; init; }
    public ICollection<User> Followers { get; init; }
}