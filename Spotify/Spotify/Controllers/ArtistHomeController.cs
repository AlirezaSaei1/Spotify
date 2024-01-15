using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotify.Data;
using Spotify.Models;
using Spotify.Storage;

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
        return View("Index");
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