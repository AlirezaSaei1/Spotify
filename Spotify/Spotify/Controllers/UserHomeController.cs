using Microsoft.AspNetCore.Mvc;

namespace Spotify.Controllers;

public class UserHomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}