using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{

public class CreateProductDto
    {
        [Required]
       public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }
        [Required]
        public string ProductTypeId { get; set; } = string.Empty;
        [Required]
        public string ProductBrandId { get; set; } = string.Empty;
        [Required]
        public string PictureUrl { get; set; } = string.Empty;
        [Range(0, int.MaxValue, ErrorMessage = "Quantity in stock cannot be negative.")]
        public int QuantityInStock { get; set; }
    
    
    }

}