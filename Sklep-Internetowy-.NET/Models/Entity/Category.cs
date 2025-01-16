namespace Sklep_Internetowy_.NET.Models.Entity
{
    public class Category
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public Guid? ParentCategoryId { get; set; }
        public Category? ParentCategory { get; set; }

        public ICollection<Category> SubCategories { get; set; } = new List<Category>();

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
