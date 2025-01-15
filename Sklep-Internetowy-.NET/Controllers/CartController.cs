using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy_.NET.Models.Entity;
using Sklep_Internetowy_.NET.Models.ViewModel;
using System.Text.Json;
using test_do_projektu.Data;

namespace Sklep_Internetowy_.NET.Controllers
{
    public class CartController : Controller
    {
        private const string CartCookieKey = "Cart";
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cart = GetCartFromCookies();
            return View(cart);
        }

        public IActionResult AddToCart(Guid productId)
        {
            var product = _context.Products.Find(productId);
            if (product == null)
            {
                return NotFound();
            }

            var cart = GetCartFromCookies();
            var cartItem = cart.FirstOrDefault(p => p.Product.Id == productId);

            if (cartItem == null)
            {
                cart.Add(new CartItem { Product = product, Quantity = 1 });
            }
            else
            {
                cartItem.Quantity++;
            }

            SaveCartToCookies(cart);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateCart([FromBody] UpdateCartRequest request)
        {
            // Pobranie koszyka z cookies (lub sesji)
            var cart = GetCartFromCookies();

            // Znalezienie produktu w koszyku
            var cartItem = cart.FirstOrDefault(c => c.Product.Id.Equals(request.ProductId));
            if (cartItem != null)
            {
                // Aktualizacja ilości produktu
                cartItem.Quantity = request.Quantity;

                // Zapisanie zaktualizowanego koszyka do cookies
                SaveCartToCookies(cart);
            }

            // Zwrócenie statusu OK
            return Ok(new { success = true });
        }

        public IActionResult RemoveFromCart(Guid productId)
        {
            var cart = GetCartFromCookies();
            var cartItem = cart.FirstOrDefault(p => p.Product.Id == productId);

            if (cartItem != null)
            {
                cart.Remove(cartItem);
            }

            SaveCartToCookies(cart);
            return RedirectToAction("Index");
        }

        private List<CartItem> GetCartFromCookies()
        {
            var cookie = Request.Cookies[CartCookieKey];
            if (cookie == null)
            {
                return new List<CartItem>();
            }

            return JsonSerializer.Deserialize<List<CartItem>>(cookie) ?? new List<CartItem>();
        }

        private void SaveCartToCookies(List<CartItem> cart)
        {
            var cartJson = JsonSerializer.Serialize(cart);
            Response.Cookies.Append(CartCookieKey, cartJson, new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7)
            });
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            var cart = GetCartFromCookies();
            if (!cart.Any())
            {
                return RedirectToAction("Index", "Cart");
            }

            if (User.Identity.IsAuthenticated)
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
                if (user != null)
                {
                    var model = new CheckoutViewModel
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Address = user.Address,
                        City = user.City,
                        Zip = user.Zip,
                        Country = user.Country
                    };
                    return View(model);
                }
            }

            return View(new CheckoutViewModel());
        }

        [HttpPost]
        public IActionResult Checkout(CheckoutViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.IsAuthenticated
                    ? _context.Users.FirstOrDefault(u => u.Email == User.Identity.Name)
                    : null;

                var order = new Order
                {
                    CreatedData = DateTime.UtcNow,
                    Status = _context.OrderStatuses.FirstOrDefault(p => p.StatusName == "Nowe"),
                    PaymentMethod = null,
                    ShippingMethod = null,
                    User = user,
                    //Products = new List<Product>(),
                    UserId = user?.Id ?? null
                };

                var cart = GetCartFromCookies();
                foreach (var cartItem in cart)
                {
                    var product = _context.Products.FirstOrDefault(p => p.Id == cartItem.Product.Id);
                    if (product != null)
                    {
                        order.Products.Add(product);
                    }
                }

                _context.Orders.Add(order);
                _context.SaveChanges();

                Response.Cookies.Delete(CartCookieKey);

                return RedirectToAction("Payment", new { orderId = order.Id });
            }

            return View(model);
        }

        public IActionResult Payment(int orderId)
        {
            var order = _context.Orders.Include(o => o.Products).FirstOrDefault(o => o.Id == orderId);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
    }
}
