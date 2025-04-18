using Microsoft.AspNetCore.Mvc;

namespace MusicalInstrumentShop.Controllers
{
    public class RepairController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RequestService()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RequestService(string name, string email, string instrumentType, string description)
        {
            // This would normally create a new repair request in the database
            // For now, just redirect to the home page with a success message
            TempData["Message"] = "Your repair request has been submitted successfully.";
            return RedirectToAction("Index", "Home");
        }
    }
}