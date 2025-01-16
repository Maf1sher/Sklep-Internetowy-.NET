using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy_.NET.Areas.Admin.Models
{
    public class ProductEditViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Product name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative value.")]
        public int Quantity { get; set; }

        public string? ExistingImagePath { get; set; }

        [Display(Name = "Product Image")]
        public IFormFile? ImageFile { get; set; }
    }
}
