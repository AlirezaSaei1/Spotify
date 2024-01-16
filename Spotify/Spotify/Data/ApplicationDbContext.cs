using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Spotify.Models;

namespace Spotify.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Music> Musics { get; init; }
    public DbSet<ArtistFollower> ArtistFollowers { get; init; }
    public DbSet<ArtistMusic> ArtistMusics { get; init; }
    public DbSet<UserFollowing> UserFollowings { get; init; }
    public DbSet<UserMusic> UserMusics { get; init; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}