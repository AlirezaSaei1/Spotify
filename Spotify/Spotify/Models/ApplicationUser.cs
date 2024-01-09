using Microsoft.AspNetCore.Identity;

namespace Spotify.Models;

public class ApplicationUser : IdentityUser
{
    public string Name { set; get; }
    public DateTime AccountCreationTime { get; set; }
    
    public ApplicationUser()
    {
        AccountCreationTime = DateTime.Now;
    } 
}