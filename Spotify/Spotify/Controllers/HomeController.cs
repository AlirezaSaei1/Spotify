using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotify.Models;

namespace Spotify.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [SuppressMessage("ReSharper.DPA", "DPA0011: High execution time of MVC action")]
        public IActionResult Index()
        {
            if (!User.Identity!.IsAuthenticated) return View();
            
            var user = _userManager.GetUserAsync(User).Result;

            if (user is User)
            {
                return RedirectToAction("Index", "UserHome");
            }
            else if (user is Artist)
            {
                return RedirectToAction("Index", "ArtistHome");
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}