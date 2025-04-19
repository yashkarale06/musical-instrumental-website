using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicalInstrumentShop.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        
        public string UserId { get; set; } = string.Empty;
        
        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }
        
        public DateTime OrderDate { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        
        public string Status { get; set; } = string.Empty;
        
        public string ShippingAddress { get; set; } = string.Empty;
        
        public string PaymentMethod { get; set; } = string.Empty;
        
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        
        public Order()
        {
            OrderDate = DateTime.UtcNow;
            OrderItems = new HashSet<OrderItem>();
            Status = "Pending";
        }
    }
    
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        
        public int OrderId { get; set; }
        
        [ForeignKey("OrderId")]
        public virtual Order? Order { get; set; }
        
        public int ProductId { get; set; }
        
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
        
        public int Quantity { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}