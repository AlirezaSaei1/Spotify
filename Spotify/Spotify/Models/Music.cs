namespace Spotify.Models;

public class Music
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Url { get; init; }
    public int Saved { get; init; }
    
    public Artist Artist { get; set; }
}