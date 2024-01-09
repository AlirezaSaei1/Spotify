using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spotify.Models;


public class Music
{
    public Music(int musicId, string name, Artist artist, DateTime creationDate)
    {
        Name = name;
        Artist = artist;
        CreationDate = creationDate;
        MusicId = musicId;
    }

    [Key]
    public int MusicId { get; set; }
    public string Name { get; set; }
    public DateTime CreationDate { get; set; }
    // Relationships
    public int ArtistId { get; set; }
    [ForeignKey("ArtistId")]
    public Artist Artist { get; set; }

}