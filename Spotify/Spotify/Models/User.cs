using System.Collections.Generic;

namespace Spotify.Models;

// alirezauser@gmail.com - Alireza.1234
public class User : ApplicationUser
{
    public ICollection<Music> SavedMusics { get; init; } = new HashSet<Music>();
    public ICollection<Artist> FollowedArtists { get; init; } = new HashSet<Artist>();

}