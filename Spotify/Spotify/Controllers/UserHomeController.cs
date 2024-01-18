using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        var userId = _userManager.GetUserAsync(User).Result!.Id;
        
        var musicList = new List<Tuple<Music, bool>>();

        if (!string.IsNullOrEmpty(searchString))
        {
            allMusics = allMusics
                .Where(m => m.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        foreach (var music in allMusics)
        {
            var hasSaved = _dbContext.UserMusics.Any(um => um.UserId == userId && um.SavedMusicId == music.Id);
            
            var tuple = new Tuple<Music, bool>(music, hasSaved);
            musicList.Add(tuple);
        }

        return View(musicList);
    }

    
    [HttpPost]
    [SuppressMessage("ReSharper.DPA", "DPA0011: High execution time of MVC action")]
    public async Task<IActionResult> SaveMusic(int musicId)
    {
        var currentUser = await _userManager.GetUserAsync(User) as User;

        var hasSavedMusic = await _dbContext.UserMusics
            .AnyAsync(um => um.UserId == currentUser!.Id && um.SavedMusicId == musicId);

        if (!hasSavedMusic)
        {
            var music = await _dbContext.Musics.FirstOrDefaultAsync(m => m.Id == musicId);

            if (music != null)
            {
                var userMusic = new UserMusic
                {
                    UserId = currentUser!.Id,
                    SavedMusicId = music.Id
                };

                _dbContext.UserMusics.Add(userMusic);
                music.Saved++;

                await _dbContext.SaveChangesAsync();
            }
        }

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

        var followedArtistIds = _dbContext.UserFollowings
            .Where(uf => uf.UserId == currentUser!.Id)
            .Select(uf => uf.FollowingArtistId)
            .ToList();

        var currentUserFollowingArtists = _dbContext.UserFollowings
            .Where(uf => uf.UserId == currentUser!.Id)
            .ToList();

        var viewModel = new ArtistsViewModel
        {
            Artists = randomArtists,
            SearchString = searchString,
            CurrentUser = currentUser!,
            CurrentUserFollowingArtists = currentUserFollowingArtists,
        };

        return View(viewModel);
    }

    
    
    [SuppressMessage("ReSharper.DPA", "DPA0011: High execution time of MVC action")]
    public async Task<RedirectToActionResult> FollowArtist(string artistId)
    {
        var user = await _userManager.GetUserAsync(User) as User;
        var artistToFollow = await _userManager.FindByIdAsync(artistId) as Artist;
        
        if (!_dbContext.UserFollowings.Any(uf => uf.UserId == user!.Id && uf.FollowingArtistId == artistToFollow.Id))
        {
            var userFollowing = new UserFollowing
            {
                UserId = user!.Id,
                FollowingArtistId = artistToFollow!.Id
            };
            
            var artistFollower = new ArtistFollower
            {
                ArtistId = artistToFollow.Id,
                FollowerUsertId = user.Id
            };
            
            _dbContext.UserFollowings.Add(userFollowing);
            _dbContext.ArtistFollowers.Add(artistFollower);
            
            // user.FollowedArtists.Add(artistToFollow);
            // artistToFollow.Followers.Add(user);
            // await _userManager.UpdateAsync(user);
            // await _userManager.UpdateAsync(artistToFollow);
            
            await _dbContext.SaveChangesAsync();
        }
        
        return RedirectToAction("Artists");
    }

    [SuppressMessage("ReSharper.DPA", "DPA0011: High execution time of MVC action")]
    public async Task<RedirectToActionResult> UnfollowArtist(string artistId)
    {
        var user = await _userManager.GetUserAsync(User) as User;
        var artistToUnfollow = await _userManager.FindByIdAsync(artistId) as Artist;
        
        var userFollowing = _dbContext.UserFollowings.FirstOrDefault(uf => uf.UserId == user.Id && uf.FollowingArtistId == artistToUnfollow.Id);
        if (userFollowing != null)
        {
            _dbContext.UserFollowings.Remove(userFollowing);
            
            var artistFollower = _dbContext.ArtistFollowers.FirstOrDefault(af => af.ArtistId == artistToUnfollow.Id && af.FollowerUsertId == user.Id);
            if (artistFollower != null)
            {
                _dbContext.ArtistFollowers.Remove(artistFollower);
            }
            
            await _dbContext.SaveChangesAsync();
        }

        return RedirectToAction("Artists");
    }

    
    public async Task<IActionResult> FollowedArtists()
    {
        var user = await _userManager.GetUserAsync(User);
        
        var followedArtistIds = _dbContext.UserFollowings
            .Where(uf => uf.UserId == user!.Id)
            .Select(uf => uf.FollowingArtistId)
            .ToList();

        var followedArtists = _userManager.Users
            .OfType<Artist>()
            .Where(a => followedArtistIds.Contains(a.Id))
            .ToList();

        if (!followedArtists.Any())
        {
            ViewBag.Message = "You are following no artists.";
        }

        return View(followedArtists);
    }

    
    public IActionResult SavedMusics()
    {
        var currentUser = _userManager.GetUserAsync(User).Result as User;

        var savedMusicIds = _dbContext.UserMusics
            .Where(um => um.UserId == currentUser!.Id)
            .Select(um => um.SavedMusicId)
            .ToList();

        var savedMusics = _dbContext.Musics
            .Where(m => savedMusicIds.Contains(m.Id))
            .ToList();

        return View(savedMusics);
    }
    
    
    [HttpPost]
    [SuppressMessage("ReSharper.DPA", "DPA0011: High execution time of MVC action")]
    public async Task<IActionResult> RemoveSavedMusic(int savedMusicId)
    {
        var user = await _userManager.GetUserAsync(User) as User;
        
        var userMusicEntry = _dbContext.UserMusics
            .FirstOrDefault(um => um.UserId == user!.Id && um.SavedMusicId == savedMusicId);

        if (userMusicEntry != null)
        {
            var music = _dbContext.Musics.FirstOrDefault(m => m.Id == savedMusicId);
            
            if (music is { Saved: > 0 })
            {
                music.Saved--;
                _dbContext.UserMusics.Remove(userMusicEntry);
                await _dbContext.SaveChangesAsync();
            }
        }

        return RedirectToAction("SavedMusics");
    }
}