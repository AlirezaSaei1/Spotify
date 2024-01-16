namespace Spotify.Models;

public class Music
{
    private static int _nextId = 10000;
    public int Id { get; init; }
    public string Name { get; init; }
    public string Url { get; init; }
    public int Saved { get; set; }
    
    public Artist Artist { get; set; }
    
    public Music()
    {
        Id = GetNextId();
    }

    private static int GetNextId()
    {
        return _nextId++;
    }
}