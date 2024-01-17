namespace Spotify.Models;

public class Music
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public int Saved { get; set; }
    
    public Artist Artist { get; set; }
    
}