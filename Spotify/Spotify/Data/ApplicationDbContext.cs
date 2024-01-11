using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Spotify.Models;

namespace Spotify.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
{
    base.OnModelCreating(builder);
    
    builder.Entity<Music>()
        .HasOne(m => m.Artist)
        .WithMany(a => a.PublishedMusics)
        .HasForeignKey(m => m.ArtistId)
        .OnDelete(DeleteBehavior.Restrict);

    builder.Entity<User>()
        .HasMany(u => u.SavedMusics)
        .WithMany()
        .UsingEntity<Dictionary<string, object>>(
            "UserSavedMusic",
            j => j
                .HasOne<Music>()
                .WithMany()
                .HasForeignKey("MusicId")
                .HasConstraintName("FK_UserSavedMusic_Music_MusicId")
                .OnDelete(DeleteBehavior.Cascade),
            j => j
                .HasOne<User>()
                .WithMany()
                .HasForeignKey("UserId")
                .HasConstraintName("FK_UserSavedMusic_AspNetUsers_UserId")
                .OnDelete(DeleteBehavior.Cascade),
            j =>
            {
                j.HasKey("UserId", "MusicId");
                j.HasIndex("MusicId");
                j.ToTable("UserSavedMusics");
            }
        );

    builder.Entity<Artist>()
        .HasMany(a => a.Followers)
        .WithMany(u => u.FollowedArtists)
        .UsingEntity<Dictionary<string, object>>(
            "ArtistFollower",
            j => j
                .HasOne<User>()
                .WithMany()
                .HasForeignKey("UserId")
                .HasConstraintName("FK_ArtistFollower_AspNetUsers_UserId")
                .OnDelete(DeleteBehavior.Cascade),
            j => j
                .HasOne<Artist>()
                .WithMany()
                .HasForeignKey("ArtistId")
                .HasConstraintName("FK_ArtistFollower_Artists_ArtistId")
                .OnDelete(DeleteBehavior.Cascade),
            j =>
            {
                j.HasKey("UserId", "ArtistId");
                j.HasIndex("ArtistId");
                j.ToTable("ArtistFollowers");
            }
        );
    }
}