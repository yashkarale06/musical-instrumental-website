using System;
using System.ComponentModel.DataAnnotations;

namespace MusicalInstrumentShop.ViewModels
{
    public class FeedbackViewModel
    {
        [Required]
        [Display(Name = "Your Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Subject")]
        public string Subject { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Message")]
        [MinLength(10, ErrorMessage = "Message must be at least 10 characters long")]
        public string Message { get; set; } = string.Empty;

        [Display(Name = "Subscribe to Newsletter")]
        public bool SubscribeToNewsletter { get; set; }
    }

    public class ReviewViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Your Name")]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Range(1, 5)]
        [Display(Name = "Rating (1-5)")]
        public int Rating { get; set; }

        [Required]
        [Display(Name = "Review Title")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(10, ErrorMessage = "Comment must be at least 10 characters long")]
        [Display(Name = "Your Review")]
        public string Comment { get; set; } = string.Empty;

        public DateTime DatePosted { get; set; } = DateTime.Now;
    }
}