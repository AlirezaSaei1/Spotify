namespace Spotify.Models;

public class Artist
{
    public Artist(int artistId, string name, string? bio, List<Music> musics)
    {
        ArtistId = artistId;
        Name = name;
        Bio = bio;
        Musics = musics;
    }

    public int ArtistId { get; set; }
    public string Name { get; set; }
    public string? Bio { get; set; }
    public List<Music> Musics { get; set; }
    
}