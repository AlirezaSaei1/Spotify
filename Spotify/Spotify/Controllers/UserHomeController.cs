using System;
using System.Linq;
using System.Threading.Tasks;
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
        var currentUser = _userManager.GetUserAsync(User).Result;
        var currentDate = DateTime.Now;
        var accountCreationTime = currentUser!.AccountCreationTime;
        var accountAge = (currentDate - accountCreationTime).Days;
    
        ViewData["CurrentDate"] = currentDate;
        ViewData["AccountAge"] = accountAge;
        
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
        
        if (!allArtists.Any())
        {
            ViewBag.Message = "No artists found.";
        }
        
        if (!string.IsNullOrEmpty(searchString))
        {
            allArtists = allArtists!.Where(a =>
                    a.FirstName.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    a.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        
        var randomArtists = allArtists.Take(5).ToList();

        var viewModel = new ArtistsViewModel
        {
            Artists = randomArtists,
            SearchString = searchString,
            CurrentUser = (User)currentUser
        };

        return View(viewModel);
    }
    
    public  RedirectToActionResult FollowArtist(string artistId)
    {
        Console.WriteLine("--------------UserFollowButton--------------");
        return RedirectToAction("Artists");
    }

    public RedirectToActionResult UnfollowArtist(string artistId)
    {
        Console.WriteLine("--------------UserUnfollowButton--------------");
        return RedirectToAction("Artists");
    }
    
    public async Task<IActionResult> FollowedArtists()
    {
        var user = await _userManager.GetUserAsync(User);
        
        var followedArtists = (user as User)?.FollowedArtists;

        if (followedArtists == null || !followedArtists.Any())
        {
            ViewBag.Message = "You are following no artists.";
        }

        return View(user);
    }
    
    public IActionResult SavedMusics()
    {
        return View();
    }
}