using Microsoft.AspNetCore.Identity;

namespace Spotify.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { set; get; }
    public string LastName { set; get; }
    public DateTime AccountCreationTime { get; set; }
    
    public ApplicationUser()
    {
        AccountCreationTime = DateTime.UtcNow;
    } 
}