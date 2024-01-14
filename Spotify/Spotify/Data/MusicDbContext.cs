using Microsoft.EntityFrameworkCore;
using Spotify.Models;

namespace Spotify.Data;

public class MusicDbContext : DbContext
{
    private DbSet<Music> Musics { get; set; }
    
    public MusicDbContext(DbContextOptions<MusicDbContext> options)
        : base(options)
    {
    }
}