namespace Spotify.Models;

public class ArtistsViewModel
{
    public List<Artist> Artists { get; init; }
    public string SearchString { get; init; }
    public User CurrentUser { get; init; }
    public List<UserFollowing> CurrentUserFollowingArtists { get; init; }
}