using System.ComponentModel.DataAnnotations;

namespace Spotify.Models;

public class Artist
{
    public Artist(int artistId, string name, string? bio, List<Music> musics, List<ArtistUser> followers)
    {
        ArtistId = artistId;
        Name = name;
        Bio = bio;
        Musics = musics;
        Followers = followers;
    }
    
    [Key]
    public int ArtistId { get; set; }
    public string Name { get; set; }
    public string? Bio { get; set; }
    
    // Relationships
    public List<Music> Musics { get; set; }
    public List<ArtistUser>? Followers { get; set; }
}