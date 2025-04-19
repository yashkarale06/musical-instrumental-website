using Microsoft.AspNetCore.Mvc;
using MusicalInstrumentShop.Models;
using MusicalInstrumentShop.ViewModels;
using System;
using System.Collections.Generic;

namespace MusicalInstrumentShop.Controllers
{
    public class FeedbackController : Controller
    {
        public IActionResult Feedback()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitFeedback(FeedbackViewModel model)
        {
            if (ModelState.IsValid)
            {
                // In a real app, save the feedback to database
                // For now, just show a success message
                TempData["SuccessMessage"] = "Thank you for your feedback! We appreciate your input.";
                return RedirectToAction("Feedback");
            }
            return View("Feedback", model);
        }

        public IActionResult Reviews()
        {
            // In a real app, get reviews from database
            // For now, using sample data
            var reviews = new List<ReviewViewModel>
            {
                new ReviewViewModel 
                { 
                    Id = 1, 
                    CustomerName = "Rahul Sharma", 
                    Rating = 5, 
                    Title = "Excellent store and service!", 
                    Comment = "I bought a guitar here and the quality is amazing. The customer service was also very helpful.", 
                    DatePosted = DateTime.Now.AddDays(-5)
                },
                new ReviewViewModel 
                { 
                    Id = 2, 
                    CustomerName = "Priya Patel", 
                    Rating = 4, 
                    Title = "Great selection of instruments", 
                    Comment = "They have a wide variety of instruments to choose from. Prices are reasonable too.", 
                    DatePosted = DateTime.Now.AddDays(-12)
                },
                new ReviewViewModel 
                { 
                    Id = 3, 
                    CustomerName = "Amit Kumar", 
                    Rating = 5, 
                    Title = "Fast delivery and excellent packaging", 
                    Comment = "I ordered a keyboard online and it arrived quickly and in perfect condition.", 
                    DatePosted = DateTime.Now.AddDays(-20)
                }
            };

            return View(reviews);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitReview(ReviewViewModel model)
        {
            if (ModelState.IsValid)
            {
                // In a real app, save the review to database
                // For now, just show a success message
                TempData["SuccessMessage"] = "Thank you for your review! It will be published soon.";
                return RedirectToAction("Reviews");
            }
            
            // If model is invalid, return to the reviews page
            // In a real app, you'd pass the existing reviews back too
            return RedirectToAction("Reviews");
        }
    }
}