using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy_.NET.Models.Entity;
using test_do_projektu.Data;

namespace Sklep_Internetowy_.NET.Service
{
    public class CategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Category> GetRootCategories()
        {
            return _context.Categories
                .Where(c => c.ParentCategoryId == null)
                .Include(c => c.SubCategories)
                .ToList();
        }
    }

}
