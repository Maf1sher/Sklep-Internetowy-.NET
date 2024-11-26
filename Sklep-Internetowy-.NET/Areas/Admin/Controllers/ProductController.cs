using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy_.NET.Models.Dto;
using Sklep_Internetowy_.NET.Models.Entity;
using test_do_projektu.Data;

namespace Sklep_Internetowy_.NET.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "ADMIN")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ProductController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddProductRequest request)
        {
            Product product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Quantity = request.Quantity
            };

            dbContext.Products.Add(product);
            dbContext.SaveChanges();

            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            var produckt = dbContext.Products.Find(id);


            if (produckt == null)
                return NotFound();
            dbContext.Products.Remove(produckt);
            dbContext.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult List()
        {
            List<Product> products = dbContext.Products.ToList();

            return View(products);
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            Product product = dbContext.Products.Find(id);
            return View(product);
        }
    }
}
