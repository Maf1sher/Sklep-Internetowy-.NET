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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            this.dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProductRequest request)
        {
            string imagePath = null;
            if (request.Image != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.Image.FileName);
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                imagePath = Path.Combine("images", fileName);

                using (var fileStream = new FileStream(Path.Combine(uploadDir, fileName), FileMode.Create))
                {
                    await request.Image.CopyToAsync(fileStream);
                }
            }

            Product product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Quantity = request.Quantity,
                ImagePath = imagePath
            };

            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();

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
