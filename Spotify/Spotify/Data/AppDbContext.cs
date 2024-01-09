using Microsoft.EntityFrameworkCore;
using Spotify.Models;

namespace Spotify.Data;


public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ArtistUser>()
            .HasKey(au => new {au.ArtistId, au.UserId});
        modelBuilder.Entity<ArtistUser>()
            .HasKey(au => new {au.ArtistId, au.UserId});
        modelBuilder.Entity<ArtistUser>().HasOne(au => au.Artist)
            .WithMany(a => a.Followers)
            .HasForeignKey(au => au.ArtistId);
        modelBuilder.Entity<ArtistUser>().HasOne(au => au.User)
            .WithMany(u => u.Followings)
            .HasForeignKey(au => au.UserId);
        
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Artist> Artists { get; set; }
    public DbSet<ArtistUser> ArtistUsers { get; set; }
    public DbSet<Music> Musics { get; set; }
    public DbSet<User> Users { get; set; }
}