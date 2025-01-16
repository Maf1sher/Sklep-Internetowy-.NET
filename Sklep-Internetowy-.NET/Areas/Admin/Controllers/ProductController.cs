using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy_.NET.Areas.Admin.Models;
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

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            var product = dbContext.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            dbContext.Products.Remove(product);
            dbContext.SaveChanges();

            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var product = dbContext.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new ProductEditViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity,
                ExistingImagePath = product.ImagePath
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(ProductEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var product = dbContext.Products.Find(viewModel.Id);
                if (product == null)
                {
                    return NotFound();
                }

                product.Name = viewModel.Name;
                product.Description = viewModel.Description;
                product.Price = viewModel.Price;
                product.Quantity = viewModel.Quantity;

                if (viewModel.ImageFile != null)
                {
                    var fileName = Path.GetFileName(viewModel.ImageFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        viewModel.ImageFile.CopyTo(stream);
                    }

                    product.ImagePath = "images/" + fileName;
                }

                dbContext.Products.Update(product);
                dbContext.SaveChanges();
                return RedirectToAction("Details", new { id = product.Id });
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult DiscountProduct(Guid id, decimal newPrice)
        {
            var product = dbContext.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            if (!product.IsOnSale)
            {
                product.PreviousPrice = product.Price;
            }

            product.Price = newPrice;
            product.IsOnSale = true;

            dbContext.Products.Update(product);
            dbContext.SaveChanges();

            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult RemoveDiscount(Guid id)
        {
            var product = dbContext.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            if (product.IsOnSale)
            {
                product.Price = product.PreviousPrice ?? product.Price;
                product.PreviousPrice = null;
                product.IsOnSale = false;

                dbContext.Products.Update(product);
                dbContext.SaveChanges();
            }

            return RedirectToAction("List");
        }

    }
}
