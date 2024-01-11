namespace Spotify.Models;

public class Artist : ApplicationUser
{
    public List<Music> PublishedMusics { get; init; }
    public List<User> Followers { get; init; }
}