using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sklep_Internetowy_.NET.Models.Entity;
using Sklep_Internetowy_.NET.Models.ViewModel;
using test_do_projektu.Data;

namespace Sklep_Internetowy_.NET.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "ADMIN")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult List()
        {
            var categories = dbContext.Categories.ToList();
            return View(categories);
        }

        public IActionResult Add()
        {
            var categories = dbContext.Categories.ToList();

            var model = new CategoryViewModel
            {
                Categories = categories
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Add(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Id = Guid.NewGuid(),
                    Name = model.Name,
                    ParentCategoryId = model.ParentCategoryId
                };


                dbContext.Categories.Add(category);
                dbContext.SaveChanges();
                return RedirectToAction("List");
            }

            model.Categories = dbContext.Categories.ToList();
            return View(model);
        }

        public IActionResult Edit(Guid id)
        {
            var category = dbContext.Categories
                .FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            var categories = dbContext.Categories
                .ToList();

            var model = new EditCategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                ParentCategoryId = category.ParentCategoryId,
                Categories = categories
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = dbContext.Categories
                    .FirstOrDefault(c => c.Id == model.Id);

                if (category == null)
                {
                    return NotFound();
                }

                category.Name = model.Name;
                category.ParentCategoryId = model.ParentCategoryId;

                dbContext.Categories.Update(category);
                dbContext.SaveChanges();

                return RedirectToAction("List");
            }

            model.Categories = dbContext.Categories
                .Where(c => c.ParentCategoryId == null).ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            var category = dbContext.Categories.Find(id);
            if (category != null)
            {
                dbContext.Categories.Remove(category);
                dbContext.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}
