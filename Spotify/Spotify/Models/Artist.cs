using System.Collections.Generic;

namespace Spotify.Models;

// alirezaartist@gmail.com - Alireza.1234
// arthur@gmail.com - Arthur.1234
public class Artist : ApplicationUser
{
    public ICollection<Music> PublishedMusics { get; init; } = new HashSet<Music>();
    public ICollection<User> Followers { get; init; } = new HashSet<User>();

}