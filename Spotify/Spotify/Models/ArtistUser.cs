namespace Spotify.Models;

public class ArtistUser
{
    public ArtistUser(int artistId, Artist artist, int userId, User user)
    {
        ArtistId = artistId;
        Artist = artist;
        UserId = userId;
        User = user;
    }

    public int ArtistId { get; set; }
    public Artist Artist { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    
}