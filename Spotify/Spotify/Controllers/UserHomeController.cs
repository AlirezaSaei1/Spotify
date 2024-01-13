using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotify.Models;
using Spotify.ViewModels;

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
    
    public IActionResult Artists(string searchString)
    {
        var currentUser = _userManager.GetUserAsync(User).Result;
        var allArtists = _userManager.Users.OfType<Artist>().ToList();
        
        if (allArtists == null || !allArtists.Any())
        {
            ViewBag.Message = "No artists found.";
        }

        // Filter artists based on search string
        if (!string.IsNullOrEmpty(searchString))
        {
            allArtists = allArtists!.Where(a =>
                    a.FirstName.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    a.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        
        var randomArtists = allArtists!.OrderBy(a => Guid.NewGuid()).Take(5).ToList();

        var viewModel = new ArtistsViewModel
        {
            Artists = randomArtists,
            SearchString = searchString,
            CurrentUser = currentUser!
        };

        return View(viewModel);
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