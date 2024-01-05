namespace Spotify.Core.Models;

public class User
{
    public int Id { get; init; }
    public required string Username { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
    public bool IsEmailVerified { get; init; }
    
    public ICollection<Song> FavoriteSongs { get; init; }
    public ICollection<Artist> FollowedArtists { get; init; }
}