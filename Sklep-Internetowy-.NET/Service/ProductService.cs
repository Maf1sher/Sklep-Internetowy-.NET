using Sklep_Internetowy_.NET.Models.Entity;
using test_do_projektu.Data;

namespace Sklep_Internetowy_.NET.Service
{
    public interface IProductService
    {
        IEnumerable<Product> GetProductsByCategory(Guid categoryId);
    }

    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetProductsByCategory(Guid categoryId)
        {
            return _context.Products.Where(p => p.CategoryId == categoryId).ToList();
        }
    }
}
