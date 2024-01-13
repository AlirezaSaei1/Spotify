namespace Spotify.Models;

// alirezaartist@gmail.com - Alireza.1234
// arthur@gmail.com - Arthur.1234
public class Artist : ApplicationUser
{
    public List<Music> PublishedMusics { get; init; }
    public List<User> Followers { get; init; }
}