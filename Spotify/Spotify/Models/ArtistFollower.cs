using System.ComponentModel.DataAnnotations;

namespace Spotify.Models;

public class ArtistFollower
{
    [Key]
    public int ID { get; set; }
    
    public string ArtistId { get; set; }
    public string FollowerUsertId { get; set; }
}