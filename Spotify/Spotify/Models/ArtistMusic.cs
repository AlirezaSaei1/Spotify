using System.ComponentModel.DataAnnotations;

namespace Spotify.Models;

public class ArtistMusic
{
    [Key]
    public int ID { get; set; }
    
    public string ArtistId { get; set; }
    public int PublishedMusicId { get; set; }
}