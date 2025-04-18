using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicalInstrumentShop.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        
        [Required]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        
        public string Brand { get; set; } = string.Empty;
        
        public string Model { get; set; } = string.Empty;
        
        public int StockQuantity { get; set; }
        
        public int CategoryId { get; set; }
        
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
        
        public string ImageUrl { get; set; } = "/images/products/default.jpg";
    }
}