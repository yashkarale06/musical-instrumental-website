using System.ComponentModel.DataAnnotations;

namespace MusicalInstrumentShop.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        
        [Required]
        public string Name { get; set; } = string.Empty;
        
        public string Description { get; set; } = string.Empty;
        
        public virtual ICollection<Product> Products { get; set; }
        
        public Category()
        {
            Products = new HashSet<Product>();
        }
    }
}