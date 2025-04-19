using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicalInstrumentShop.Models;
using MusicalInstrumentShop.Services;
using MusicalInstrumentShop.ViewModels;
using System;
using System.Threading.Tasks;

namespace MusicalInstrumentShop.Controllers
{
    public class CartController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RazorpayService _razorpayService;
        private readonly IConfiguration _configuration;

        public CartController(
            UserManager<ApplicationUser> userManager, 
            RazorpayService razorpayService,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _razorpayService = razorpayService;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            // This would normally get the cart items from a database or session
            if (TempData["CartMessage"] != null)
            {
                ViewBag.CartMessage = TempData["CartMessage"]?.ToString();
            }
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
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                // If not authenticated, redirect to login page with return URL
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Checkout", "Cart") });
            }

            // In a real app, this would come from the cart calculation
            decimal orderAmount = 75723.00m; // Total order amount

            try
            {
                // Create a Razorpay order
                string orderId = _razorpayService.CreateOrder(orderAmount, "INR", $"ORDER_{DateTime.Now.Ticks}");

                // Store this order ID somewhere (session, TempData, etc.) for verification later
                TempData["RazorpayOrderId"] = orderId;
                
                string customerName = User.Identity.Name ?? "Customer";
                string customerEmail = User.Identity.Name ?? "customer@example.com";
                
                var razorpayViewModel = new RazorpayCheckoutViewModel
                {
                    OrderId = orderId,
                    Amount = orderAmount,
                    Currency = "INR",
                    Name = "Musical Instrument Shop",
                    Description = "Purchase from Musical Instrument Shop",
                    ImageLogo = "/images/logo.png", 
                    RazorpayKey = _configuration["Razorpay:KeyId"] ?? "rzp_test_key",
                    CustomerName = customerName,
                    CustomerEmail = customerEmail,
                    CustomerPhone = "9999999999" // In a real app, get actual phone
                };

                return View(razorpayViewModel);
            }
            catch (Exception ex)
            {
                // Log the error in a real application
                TempData["ErrorMessage"] = "Payment initialization failed: " + ex.Message;
                return RedirectToAction("Index", "Cart");
            }
        }

        [Authorize] // This action requires authentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PaymentCallback(string razorpay_payment_id, string razorpay_order_id, string razorpay_signature)
        {
            // Get the expected order ID from TempData
            string? storedOrderId = TempData["RazorpayOrderId"] as string;
            
            if (string.IsNullOrEmpty(storedOrderId) || storedOrderId != razorpay_order_id)
            {
                return BadRequest("Invalid order");
            }

            // Verify the payment signature
            bool isValidSignature = _razorpayService.VerifyPaymentSignature(razorpay_order_id, razorpay_payment_id, razorpay_signature);
            
            if (!isValidSignature)
            {
                return BadRequest("Invalid payment signature");
            }

            // Get the current user
            var user = await _userManager.GetUserAsync(User);
            
            if (user == null)
            {
                return NotFound();
            }

            // In a real application, you would:
            // 1. Create an order record in your database
            // 2. Clear the cart
            // 3. Send an order confirmation email
            
            // Store success message and payment ID
            TempData["PaymentId"] = razorpay_payment_id;
            TempData["SuccessMessage"] = "Your payment was successful and your order has been placed!";
            
            return RedirectToAction("OrderConfirmation");
        }

        [Authorize] // This action requires authentication
        public IActionResult OrderConfirmation()
        {
            ViewBag.PaymentId = TempData["PaymentId"] as string;
            return View();
        }
    }
}