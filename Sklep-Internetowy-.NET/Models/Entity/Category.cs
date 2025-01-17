using System.Text.Json.Serialization;

namespace Sklep_Internetowy_.NET.Models.Entity
{
    public class Category
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public Guid? ParentCategoryId { get; set; }
        public Category? ParentCategory { get; set; }

        [JsonIgnore]
        public List<Category> SubCategories { get; set; } = new List<Category>();

        [JsonIgnore]
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
