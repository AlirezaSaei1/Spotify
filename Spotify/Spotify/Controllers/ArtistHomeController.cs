using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotify.Data;
using Spotify.Models;
using Spotify.Services;

namespace Spotify.Controllers;

public class ArtistHomeController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _dbContext;

    public ArtistHomeController(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
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
    
    public async Task<IActionResult> Musics()
    {
        var artist = await _userManager.GetUserAsync(User) as Artist;
        var artistMusics = _dbContext.Musics
            .Where(music => music.Artist.Id == artist!.Id)
            .ToList();
        
        return View(artistMusics);
    }
    
    public IActionResult NewMusic()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> UploadMusic(IFormFile musicFile)
    {
        var artist = await _userManager.GetUserAsync(User) as Artist;
        
        var newMusic = new Music
        {
            Name = musicFile.FileName,
            Artist = artist!,
            Saved = 0
        };
        var musicStorageService = new MusicStorageService();
        var s3Url = musicStorageService.UploadObjectFromFile(musicFile, $"{newMusic.Id}.mp3");
        newMusic.Url = s3Url;
        
        _dbContext.Musics.Add(newMusic);
        await _dbContext.SaveChangesAsync();
        
        return RedirectToAction("Index");
    }
    
    
    public async Task<IActionResult> Followers()
    {
        var user = await _userManager.GetUserAsync(User);

        var artistFollowers = _dbContext.ArtistFollowers
            .Where(af => af.ArtistId == user!.Id)
            .Select(af => af.FollowerUsertId)
            .ToList();

        var followers = _userManager.Users.OfType<User>()
            .Where(u => artistFollowers.Contains(u.Id))
            .ToList();

        if (!followers.Any())
        {
            ViewBag.Message = "You have no followers";
        }

        return View(followers);
    }

}