using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace MusicalInstrumentShop.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime DateRegistered { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }
        
        // Navigation property for orders
        public virtual ICollection<Order> Orders { get; set; }
        
        public ApplicationUser()
        {
            DateRegistered = DateTime.UtcNow;
            Orders = new HashSet<Order>();
        }
    }
}