using System.ComponentModel.DataAnnotations;

namespace Spotify.Models;

public class UserFollowing
{
    [Key]
    public int ID { get; set; }
    
    public string UserId { get; set; }
    public string FollowingArtistId { get; set; }
}