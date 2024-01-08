namespace Spotify.Models;


public class Music
{
    public Music(string name, Artist artist, DateTime creationDate)
    {
        Name = name;
        Artist = artist;
        CreationDate = creationDate;
    }

    public string Name { get; set; }
    public Artist Artist { get; set; }
    public DateTime CreationDate { get; set; }
    
}