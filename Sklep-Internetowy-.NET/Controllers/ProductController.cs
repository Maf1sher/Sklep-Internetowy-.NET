using Microsoft.AspNetCore.Mvc;
using Sklep_Internetowy_.NET.Models.ViewModel;
using Sklep_Internetowy_.NET.Models.Entity;
using test_do_projektu.Data;
using Microsoft.EntityFrameworkCore;

namespace Sklep_Internetowy_.NET.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ProductController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            List<Product> products = dbContext.Products.ToList();

            var newProducts = dbContext.Products
                .Where(p => p.CreatedData >= DateTime.UtcNow.AddDays(-5))
                .OrderByDescending(p => p.CreatedData)
                .Take(5)
                .ToList();

            var onSaleProducts = dbContext.Products
                .Where(p => p.IsOnSale)
                .OrderByDescending(p => p.CreatedData)
                .ToList();

            var model = new ProductListViewModel
            {
                AllProducts = products,
                NewProducts = newProducts,
                OnSaleProducts = onSaleProducts
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            Product product = dbContext.Products.Find(id);
            return View(product);
        }


        public IActionResult AddToCard(Guid id)
        {

            return View();
        }

        public IActionResult OnSaleProducts()
        {
            var onSaleProducts = dbContext.Products
                .Where(p => p.IsOnSale)
                .ToList();

            return View(onSaleProducts);
        }

        public IActionResult TopSelling()
        {
            var topSellingProducts = dbContext.Products
                .Select(p => new TopSellingProductViewModel
                {
                    Product = p,
                    SoldQuantity = p.OrderProducts.Sum(op => op.Quantity)
                })
                .OrderByDescending(x => x.SoldQuantity)
                .Take(10)
                .ToList();

            return View(topSellingProducts);
        }
    }
}
