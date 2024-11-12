using Microsoft.AspNetCore.Mvc;
using Sklep_Internetowy_.NET.Models.Dto;
using Sklep_Internetowy_.NET.Models.Entity;
using test_do_projektu.Data;

namespace Sklep_Internetowy_.NET.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext dbContext;

        public ProductController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
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

            return View();
        }

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
