using Microsoft.AspNetCore.Mvc;

namespace MusicalInstrumentShop.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            // This would normally validate the user credentials against a database
            // For now, just redirect to the home page
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string email, string password)
        {
            // This would normally create a new user in the database
            // For now, just redirect to the login page
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            // This would normally sign the user out
            return RedirectToAction("Index", "Home");
        }
    }
}