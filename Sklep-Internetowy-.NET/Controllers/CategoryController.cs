using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy_.NET.Models.ViewModel;
using test_do_projektu.Data;

namespace Sklep_Internetowy_.NET.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(Guid categoryId)
        {
            var category = _context.Categories
                .Include(c => c.SubCategories)
                .Include(c => c.Products)
                .FirstOrDefault(c => c.Id == categoryId);

            if (category == null)
            {
                return NotFound();
            }

            var viewModel = new UserCategoryViewModel
            {
                Category = category,
                Products = (List<Models.Entity.Product>)category.Products,
                SubCategories = (List<Models.Entity.Category>)category.SubCategories
            };

            return View(viewModel);
        }
    }


}
