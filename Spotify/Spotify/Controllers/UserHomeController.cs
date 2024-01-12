using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotify.Models;

namespace Spotify.Controllers;

public class UserHomeController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserHomeController(UserManager<ApplicationUser> userManager)
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
    
    public IActionResult Artists()
    {
        return View();
    }
    
    public async Task<IActionResult> FollowedArtists()
    {
        var user = await _userManager.GetUserAsync(User);
        
        var followedArtists = (user as User)?.FollowedArtists;

        if (followedArtists == null || !followedArtists.Any())
        {
            ViewBag.Message = "You are following no artists.";
        }

        return View(followedArtists);
    }
    
    public IActionResult SavedMusics()
    {
        return View();
    }
}