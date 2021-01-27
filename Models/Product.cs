using System.ComponentModel.DataAnnotations;

namespace SuplementsStore1.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [MaxLength(100)]
        public string Category { get; set; }
    }
}
