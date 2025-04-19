using Microsoft.AspNetCore.Mvc;
using MusicalInstrumentShop.Models;
using System.Diagnostics;

namespace MusicalInstrumentShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // You would typically get this data from a database
            // For now we'll use hardcoded data
            var featuredCategories = new List<Category>
            {
                new Category { CategoryId = 1, Name = "Guitars", Description = "Acoustic, Electric, Bass" },
                new Category { CategoryId = 2, Name = "Keyboards", Description = "Digital Pianos, Synthesizers" },
                new Category { CategoryId = 3, Name = "Drums & Percussion", Description = "Drum Kits, Cymbals, Percussion" },
                new Category { CategoryId = 4, Name = "Wind Instruments", Description = "Saxophones, Flutes, Trumpets" }
            };

            var featuredProducts = new List<Product>
            {
                new Product { 
                    ProductId = 1, 
                    Name = "Acoustic Guitar", 
                    Description = "Premium wooden acoustic guitar", 
                    Price = 22499, 
                    ImageUrl = "/images/products/electric-guitar.jpg" 
                },
                new Product { 
                    ProductId = 4, 
                    Name = "Digital Piano", 
                    Description = "88-key weighted digital piano", 
                    Price = 41249, 
                    ImageUrl = "/images/products/electric-guitar.jpg" 
                },
                new Product { 
                    ProductId = 7, 
                    Name = "Drum Kit", 
                    Description = "Complete 5-piece drum set", 
                    Price = 52499, 
                    ImageUrl = "/images/products/electric-guitar.jpg" 
                },
                new Product { 
                    ProductId = 10, 
                    Name = "Alto Saxophone", 
                    Description = "Professional alto saxophone", 
                    Price = 59999, 
                    ImageUrl = "/images/products/electric-guitar.jpg" 
                }
            };

            ViewBag.FeaturedCategories = featuredCategories;
            ViewBag.FeaturedProducts = featuredProducts;

            return View();
        }

        public IActionResult About()
        {
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