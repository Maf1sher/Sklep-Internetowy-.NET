using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy_.NET.Areas.Admin.Models;
using Sklep_Internetowy_.NET.Models.Dto;
using Sklep_Internetowy_.NET.Models.Entity;
using System.Diagnostics;
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
            var categories = dbContext.Categories.ToList();
            var viewModel = new AddProductRequest
            {
                CategoriesList = dbContext.Categories.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Add(AddProductRequest model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState)
                {
                    var key = modelState.Key;
                    var errors = modelState.Value.Errors;

                    foreach (var error in errors)
                    {
                        Debug.WriteLine($"Key: {key}, Error: {error.ErrorMessage}");
                    }
                }
            }

            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Quantity = model.Quantity,
                    CategoryId = model.CategoryId,
                    CreatedData = DateTime.UtcNow
                };

                if (model.Image != null)
                {
                    var fileName = Path.GetFileName(model.Image.FileName);
                    var filePath = Path.Combine("wwwroot/images", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        model.Image.CopyTo(stream);
                    }
                    product.ImagePath = "images/" + fileName;
                }

                dbContext.Products.Add(product);
                dbContext.SaveChanges();

                return RedirectToAction("List");
            }
            model.CategoriesList = dbContext.Categories.ToList();

            return View(model);
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
                ExistingImagePath = product.ImagePath,
                CategoryId = product.CategoryId,
            };
            ViewBag.Categories = dbContext.Categories.ToList();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(ProductEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = dbContext.Products.Find(model.Id);
                if (product == null)
                {
                    return NotFound();
                }

                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
                product.Quantity = model.Quantity;
                product.CategoryId = model.CategoryId;

                if (model.Image != null)
                {
                    var fileName = Path.GetFileName(model.Image.FileName);
                    var filePath = Path.Combine("wwwroot/images", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        model.Image.CopyTo(stream);
                    }
                    product.ImagePath = "/images/" + fileName;
                }

                dbContext.Products.Update(product);
                dbContext.SaveChanges();

                return RedirectToAction("List");
            }

            return View(model);
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
