using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class ProductDto 
    {
        [Required]
        public Guid Guid { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal DeliveryPrice { get; set; }
    }
}
