namespace Spotify.Models;

// alirezauser@gmail.com - Alireza.1234
public class User : ApplicationUser
{
    public List<Music> SavedMusics { get; init; }
    public List<Artist> FollowedArtists { get; init; }
}