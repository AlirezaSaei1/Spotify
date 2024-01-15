using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotify.Data;
using Spotify.Models;

namespace Spotify.Controllers;

public class UserHomeController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _dbContext;

    public UserHomeController(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
    {
        _userManager = userManager;
        _dbContext = dbContext;
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
    
    public IActionResult Musics(string searchString)
    {
        var allMusics = _dbContext.Musics.ToList();
        
        if (!string.IsNullOrEmpty(searchString))
        {
            allMusics = allMusics.Where(m =>
                    m.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        
        return View(allMusics);
    }
    
    [HttpPost]
    public IActionResult SaveMusic(int musicId)
    {
        var music = _dbContext.Musics.FirstOrDefault(m => m.Id == musicId);
        var currentUser = _userManager.GetUserAsync(User).Result as User;

        if (music == null) return RedirectToAction("Musics");
        
        currentUser!.SavedMusics.Add(music);
        music.Saved++;
        _dbContext.SaveChanges();
        return RedirectToAction("Musics");

    }

    [HttpPost]
    public IActionResult DownloadMusic(int musicId)
    {
        return RedirectToAction("Musics");
    }


    public IActionResult Artists(string searchString)
    {
        var currentUser = _userManager.GetUserAsync(User).Result as User;
        var allArtists = _userManager.Users.OfType<Artist>().ToList();
        
        if (!allArtists.Any())
        {
            ViewBag.Message = "No artists found.";
        }
        
        if (!string.IsNullOrEmpty(searchString))
        {
            allArtists = allArtists.Where(a =>
                    a.FirstName.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    a.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        
        var randomArtists = allArtists.Take(5).ToList();

        var viewModel = new ArtistsViewModel
        {
            Artists = randomArtists,
            SearchString = searchString,
            CurrentUser = currentUser!
        };

        return View(viewModel);
    }
    
    [SuppressMessage("ReSharper.DPA", "DPA0011: High execution time of MVC action")]
    public async Task<RedirectToActionResult> FollowArtist(string artistId)
    {
        var user = await _userManager.GetUserAsync(User) as User;
        var artistToFollow = await _userManager.FindByIdAsync(artistId) as Artist;
        
        user!.FollowedArtists.Add(artistToFollow!);
        artistToFollow!.Followers.Add(user);
        
        await _userManager.UpdateAsync(user);
        await _userManager.UpdateAsync(artistToFollow);
        
        return RedirectToAction("Artists");
    }

    [SuppressMessage("ReSharper.DPA", "DPA0011: High execution time of MVC action")]
    public async Task<RedirectToActionResult> UnfollowArtist(string artistId)
    {
        var user = await _userManager.GetUserAsync(User) as User;
        var artistToUnfollow = await _userManager.FindByIdAsync(artistId) as Artist;
        
        user!.FollowedArtists.Remove(artistToUnfollow!);
        artistToUnfollow!.Followers.Remove(user);
            
        await _userManager.UpdateAsync(user);
        await _userManager.UpdateAsync(artistToUnfollow);

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
        var currentUser = _userManager.GetUserAsync(User).Result as User;
        var savedMusics = currentUser!.SavedMusics.ToList();
        return View(savedMusics);
    }
}