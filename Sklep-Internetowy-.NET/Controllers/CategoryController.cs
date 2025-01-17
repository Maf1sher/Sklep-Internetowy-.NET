using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Npgsql.Internal;
using Sklep_Internetowy_.NET.Models.ViewModel;
using Sklep_Internetowy_.NET.Service;
using test_do_projektu.Data;
using Table = iText.Layout.Element.Table;

namespace Sklep_Internetowy_.NET.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductService _productService;

        public CategoryController(ApplicationDbContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
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

        public IActionResult GeneratePriceList(Guid categoryId)
        {
            var products = _productService.GetProductsByCategory(categoryId);

            if (products == null || !products.Any())
            {
                return NotFound("No products found in this category.");
            }

            using (var memoryStream = new MemoryStream())
            {
                var pdfWriter = new PdfWriter(memoryStream);
                var pdfDocument = new PdfDocument(pdfWriter);
                var document = new Document(pdfDocument);

                document.Add(new Paragraph("Price List - " + products.First().Category.Name)
                             .SetFontSize(20)
                             .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                document.Add(new Paragraph("Generated on: " + DateTime.Now.ToString("dd/MM/yyyy"))
                             .SetFontSize(12)
                             .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                document.Add(new Paragraph("\n"));

                var table = new Table(2);
                table.AddHeaderCell(new Cell().Add(new Paragraph("Product Name").SetBackgroundColor(ColorConstants.LIGHT_GRAY)));
                //table.AddHeaderCell(new Cell().Add(new Paragraph("Description").SetBackgroundColor(ColorConstants.LIGHT_GRAY)));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Price (PLN)").SetBackgroundColor(ColorConstants.LIGHT_GRAY)));

                decimal totalPrice = 0;

                foreach (var product in products)
                {
                    table.AddCell(product.Name);
                    //table.AddCell(product.Description);
                    table.AddCell(product.Price.ToString("0.00") + " PLN");

                    totalPrice += product.Price;
                }

                table.AddCell(new Cell(1, 1).Add(new Paragraph("Total Price")).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
                table.AddCell(new Cell().Add(new Paragraph(totalPrice.ToString("0.00") + " PLN")));

                document.Add(table);

                document.Close();

                return File(memoryStream.ToArray(), "application/pdf", "PriceList.pdf");
            }
        }
    }
}
