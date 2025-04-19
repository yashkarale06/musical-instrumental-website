using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicalInstrumentShop.Models;

namespace MusicalInstrumentShop.Controllers
{
    public class CartController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            // This would normally get the cart items from a database or session
            return View();
        }

        public IActionResult AddToCart(int id, int quantity = 1)
        {
            // This would normally add the item to the cart in a database or session
            // For now, just redirect to the cart page
            TempData["CartMessage"] = "Item added to cart successfully!";
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int id)
        {
            // This would normally remove the item from the cart in a database or session
            // For now, just redirect to the cart page
            return RedirectToAction("Index");
        }

        public IActionResult UpdateQuantity(int id, int quantity)
        {
            // This would normally update the quantity in the cart in a database or session
            // For now, just redirect to the cart page
            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            // Check if the user is authenticated
            if (!User.Identity.IsAuthenticated)
            {
                // If not authenticated, redirect to login page with return URL
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Checkout", "Cart") });
            }

            // If authenticated, proceed to checkout
            return View();
        }

        [Authorize] // This action requires authentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder()
        {
            // Get the current user
            var user = await _userManager.GetUserAsync(User);
            
            if (user == null)
            {
                return NotFound();
            }

            // In a real application, you would:
            // 1. Get all items from the cart
            // 2. Create an order in the database
            // 3. Create order items for each cart item
            // 4. Calculate the total amount
            // 5. Process payment
            // 6. Clear the cart
            // 7. Redirect to order confirmation

            // For this simple example, we'll just show a success message
            TempData["SuccessMessage"] = "Your order has been placed successfully!";
            return RedirectToAction("OrderConfirmation");
        }

        [Authorize] // This action requires authentication
        public IActionResult OrderConfirmation()
        {
            return View();
        }
    }
}