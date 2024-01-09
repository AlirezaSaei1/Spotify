using Microsoft.AspNetCore.Identity;

namespace Spotify.Models;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
}