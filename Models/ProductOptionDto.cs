using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    /// <summary>
    /// For product option creation, we don't need to expose the 
    /// ProductId field as this has already been supplied in the route.
    /// </summary>
    public class ProductOptionCreateDto
    {
        [Required]
        public Guid Guid { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }
    }

    /// <summary>
    /// Includes the full set of fields.
    /// </summary>
    public class ProductOptionDto : ProductOptionCreateDto
    {
        [Required]
        public Guid ProductGuid { get; set; }
    }

    public class ProductOptionFieldsOnlyDto
    {
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
