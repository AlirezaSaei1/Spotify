using System.ComponentModel.DataAnnotations;

namespace Spotify.Models;

public class UserMusic
{
    [Key]
    public int ID { get; set; }
    
    public string UserId { get; set; }
    public int SavedMusicId { get; set; }
}