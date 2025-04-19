using Microsoft.AspNetCore.Mvc;
using MusicalInstrumentShop.Models;

namespace MusicalInstrumentShop.Controllers
{
    public class ProductController : Controller
    {
        // Sample product data - in a real app this would come from a database
       
        private readonly List<Product> _products = new List<Product>
        {
            new Product { ProductId = 1, Name = "Acoustic Guitar", Description = "Premium wooden acoustic guitar", Price = 22499, CategoryId = 1, Brand = "Yamaha", Model = "F310", StockQuantity = 10,ImageUrl = "/images/products/acoustic-guitar.jpg" },
            new Product { ProductId = 2, Name = "Electric Guitar", Description = "Professional electric guitar", Price = 37499, CategoryId = 1, Brand = "Fender", Model = "Stratocaster", StockQuantity = 7, ImageUrl = "/images/products/alto-saxophone.jpg" },
            new Product { ProductId = 3, Name = "Bass Guitar", Description = "4-string bass guitar", Price = 29999, CategoryId = 1, Brand = "Ibanez", Model = "GSR200", StockQuantity = 5, ImageUrl = "/images/products/bass-guitar.jpg" },
            new Product { ProductId = 4, Name = "Digital Piano", Description = "88-key weighted digital piano", Price = 41249, CategoryId = 2, Brand = "Casio", Model = "PX-160", StockQuantity = 3, ImageUrl = "/images/products/cajon.jpg" },
            new Product { ProductId = 5, Name = "Synthesizer", Description = "61-key synthesizer with built-in speakers", Price = 26249, CategoryId = 2, Brand = "Roland", Model = "JUNO-DS", StockQuantity = 4, ImageUrl = "/images/products/digital-piano.jpg" },
            new Product { ProductId = 6, Name = "MIDI Controller", Description = "25-key MIDI controller", Price = 7499, CategoryId = 2, Brand = "Akai", Model = "MPK Mini", StockQuantity = 8, ImageUrl = "/images/products/drum-kit.jpg" },
            new Product { ProductId = 7, Name = "Drum Kit", Description = "Complete 5-piece drum set", Price = 52499, CategoryId = 3, Brand = "Pearl", Model = "Export Series", StockQuantity = 2, ImageUrl = "/images/products/electric-guitar.jpg" },
            new Product { ProductId = 8, Name = "Electronic Drum Kit", Description = "Digital drum set with mesh heads", Price = 67499, CategoryId = 3, Brand = "Roland", Model = "TD-17KVX", StockQuantity = 3, ImageUrl = "/images/products/electric-guitar.jpg" },
            new Product { ProductId = 9, Name = "Cajon", Description = "Wooden percussion instrument", Price = 9749, CategoryId = 3, Brand = "Meinl", Model = "Headliner Series", StockQuantity = 6, ImageUrl = "/images/products/electric-guitar.jpg" },
            new Product { ProductId = 10, Name = "Alto Saxophone", Description = "Professional alto saxophone", Price = 59999, CategoryId = 4, Brand = "Yamaha", Model = "YAS-280", StockQuantity = 4, ImageUrl = "/images/products/electric-guitar.jpg" },
            new Product { ProductId = 11, Name = "Flute", Description = "Silver-plated flute", Price = 26249, CategoryId = 4, Brand = "Jupiter", Model = "JFL700", StockQuantity = 5, ImageUrl = "/images/products/electric-guitar.jpg" },
            new Product { ProductId = 12, Name = "Trumpet", Description = "Brass trumpet with case", Price = 37499, CategoryId = 4, Brand = "Bach", Model = "TR300", StockQuantity = 3, ImageUrl = "/images/products/electric-guitar.jpg" }
        };

        // Sample category data
        private readonly List<Category> _categories = new List<Category>
        {
            new Category { CategoryId = 1, Name = "Guitars", Description = "Acoustic, Electric, Bass" },
            new Category { CategoryId = 2, Name = "Keyboards", Description = "Digital Pianos, Synthesizers" },
            new Category { CategoryId = 3, Name = "Drums & Percussion", Description = "Drum Kits, Cymbals, Percussion" },
            new Category { CategoryId = 4, Name = "Wind Instruments", Description = "Saxophones, Flutes, Trumpets" }
        };

        public IActionResult Index(int? categoryId = null, string priceRange = null, string sortBy = null)
        {
            // Filter products by category if specified
            var products = _products;
            
            if (categoryId.HasValue && categoryId > 0)
            {
                products = products.Where(p => p.CategoryId == categoryId).ToList();
                ViewBag.SelectedCategory = _categories.FirstOrDefault(c => c.CategoryId == categoryId)?.Name;
            }
            
            // Filter by price range
            if (!string.IsNullOrEmpty(priceRange))
            {
                switch (priceRange)
                {
                    case "0-10000":
                        products = products.Where(p => p.Price <= 10000).ToList();
                        break;
                    case "10000-25000":
                        products = products.Where(p => p.Price > 10000 && p.Price <= 25000).ToList();
                        break;
                    case "25000-50000":
                        products = products.Where(p => p.Price > 25000 && p.Price <= 50000).ToList();
                        break;
                    case "50000+":
                        products = products.Where(p => p.Price > 50000).ToList();
                        break;
                }
            }
            
            // Sort products
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "price-low":
                        products = products.OrderBy(p => p.Price).ToList();
                        break;
                    case "price-high":
                        products = products.OrderByDescending(p => p.Price).ToList();
                        break;
                    case "name":
                        products = products.OrderBy(p => p.Name).ToList();
                        break;
                    // Default sorting is by popularity (already in that order)
                }
            }
            
            ViewBag.Categories = _categories;
            ViewBag.CategoryId = categoryId;
            ViewBag.PriceRange = priceRange ?? string.Empty;
            ViewBag.SortBy = sortBy ?? string.Empty;
            
            return View(products);
        }

        public IActionResult Details(int id)
        {
            var product = _products.FirstOrDefault(p => p.ProductId == id);
            
            if (product == null)
            {
                return NotFound();
            }
            
            return View(product);
        }

        public IActionResult Category(int id)
        {
            var category = _categories.FirstOrDefault(c => c.CategoryId == id);
            
            if (category == null)
            {
                return NotFound();
            }
            
            var products = _products.Where(p => p.CategoryId == id).ToList();
            
            ViewBag.CategoryId = id;
            ViewBag.CategoryName = category.Name;
            
            return View(products);
        }
    }
}