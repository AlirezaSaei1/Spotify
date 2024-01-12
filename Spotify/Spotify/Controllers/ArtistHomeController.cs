using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotify.Models;

namespace Spotify.Controllers;

public class ArtistHomeController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ArtistHomeController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Musics()
    {
        return View();
    }
    
    public IActionResult NewMusic()
    {
        return View();
    }
    
    public async Task<IActionResult> Followers()
    {
        var user = await _userManager.GetUserAsync(User);
        
        var followers = (user as Artist)?.Followers;

        if (followers == null || !followers.Any())
        {
            ViewBag.Message = "You have no followers";
        }

        return View(followers);
    }
}