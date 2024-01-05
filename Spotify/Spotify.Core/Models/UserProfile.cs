namespace Spotify.Core.Models;

public class UserProfile
{
    public int Id { get; init; }
    public string UserId { get; init; }
    public string DisplayName { get; init; }
    public string Bio { get; init; }

    public User User { get; init; }
}