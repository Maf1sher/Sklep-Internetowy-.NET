using Sklep_Internetowy_.NET.Models.Entity;

namespace Sklep_Internetowy_.NET.Models.ViewModel
{
    public class CategoryViewModel
    {
        public string Name { get; set; }
        public Guid? ParentCategoryId { get; set; }

        public List<Category> Categories { get; set; } = new List<Category>();
    }
}
