using Microsoft.AspNetCore.Mvc;

namespace Spotify.Controllers;

public class ArtistHomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}