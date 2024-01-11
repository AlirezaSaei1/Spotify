namespace Spotify.Models;

public class User : ApplicationUser
{
    public List<Music> SavedMusics { get; init; }
    public List<Artist> FollowedArtists { get; init; }
}