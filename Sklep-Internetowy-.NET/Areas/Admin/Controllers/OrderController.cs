using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_do_projektu.Data;

namespace Sklep_Internetowy_.NET.Areas.Admin.Controllers
{
    [Area("Admin")]
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
                                 .Include(o => o.Products)
                                 .OrderByDescending(o => o.CreatedData)
                                 .ToList();

            return View(orders);
        }
    }
}
