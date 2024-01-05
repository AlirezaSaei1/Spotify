namespace Spotify.Core.Models;

public class Artist
{
    public int Id { get; init; }
    public required string Username { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
    
    public ICollection<Song> Songs { get; init; }
    public ICollection<User> Followers { get; init; }
}