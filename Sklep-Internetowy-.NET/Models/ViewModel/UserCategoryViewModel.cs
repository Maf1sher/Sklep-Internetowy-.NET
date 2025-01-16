using Sklep_Internetowy_.NET.Models.Entity;

namespace Sklep_Internetowy_.NET.Models.ViewModel
{
    public class UserCategoryViewModel
    {
        public Category Category { get; set; }
        public List<Product> Products { get; set; }
        public List<Category> SubCategories { get; set; }
    }
}
