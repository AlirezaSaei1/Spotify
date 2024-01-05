namespace Spotify.Core.Models;

public class Song
{
    public int Id { get; init; }
    public string Title { get; init; }
    public ICollection<Artist> Artists { get; init; }
    public string Url { get; init; }
}