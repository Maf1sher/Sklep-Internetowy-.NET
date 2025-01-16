using Sklep_Internetowy_.NET.Models.Entity;
using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy_.NET.Areas.Admin.Models
{
    public class ProductEditViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        public IFormFile Image { get; set; }

        public string ExistingImagePath { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        public List<Category> CategoriesList { get; set; } = new List<Category>();
    }
}
