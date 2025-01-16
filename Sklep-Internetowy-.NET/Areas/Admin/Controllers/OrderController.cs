using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_do_projektu.Data;

namespace Sklep_Internetowy_.NET.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "ADMIN")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Orders()
        {
            var orders = _context.Orders
                                 .Include(o => o.User)
                                 .Include(o => o.OrderProducts)
                                    .ThenInclude(op => op.Product)
                                 .Include(o => o.Status)
                                 .Include(o => o.ShippingMethod)
                                 .Include(o => o.PaymentMethod)
                                 .OrderByDescending(o => o.CreatedData)
                                 .ToList();

            var orderStatuses = _context.OrderStatuses.ToList();

            ViewBag.OrderStatuses = orderStatuses;

            return View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int orderId, int statusId)
        {
            var order = await _context.Orders
                                       .Include(o => o.Status)
                                       .Include(o => o.OrderProducts)
                                            .ThenInclude(op => op.Product)
                                       .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return NotFound();
            }

            var status = await _context.OrderStatuses.FindAsync(statusId);
            if (status != null)
            {
                order.Status = status;

                if (status.StatusName == "Zrealizowane")
                {
                    foreach (var product in order.OrderProducts)
                    {
                        product.Product.Quantity-=product.Quantity;
                    }
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Orders");
        }

    }
}
