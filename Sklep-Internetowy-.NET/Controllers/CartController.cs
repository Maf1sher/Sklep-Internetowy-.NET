using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy_.NET.Models.Entity;
using Sklep_Internetowy_.NET.Models.ViewModel;
using System.Diagnostics;
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
            var cart = GetCartFromCookies();

            var cartItem = cart.FirstOrDefault(c => c.Product.Id.Equals(request.ProductId));
            if (cartItem != null)
            {
                cartItem.Quantity = request.Quantity;

                SaveCartToCookies(cart);
            }

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

            var model = new CheckoutViewModel();

            if (User.Identity.IsAuthenticated)
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
                if (user != null)
                {
                    model.FirstName = user.FirstName;
                    model.LastName = user.LastName;
                    model.Email = user.Email;
                    model.Address = user.Address;
                    model.City = user.City;
                    model.Zip = user.Zip;
                    model.Country = user.Country;
                }
            }
            model.ShippingMethods = _context.ShippingMethods.ToList();
            model.PaymentMethods = _context.PaymentMethods.ToList();

            return View(model);
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
                    PaymentMethodId = model.PaymentMethodId,
                    ShippingMethodId = model.ShippingMethodId,
                    User = user,
                    UserId = user?.Id ?? null
                };

                var cart = GetCartFromCookies();

                foreach (var cartItem in cart)
                {
                    var product = _context.Products.FirstOrDefault(p => p.Id == cartItem.Product.Id);

                    if (product != null)
                    {
                        var existingOrderProduct = order.OrderProducts.FirstOrDefault(op => op.ProductId == product.Id);

                        if (existingOrderProduct != null)
                        {
                            existingOrderProduct.Quantity += cartItem.Quantity;
                        }
                        else
                        {
                            var orderProduct = new OrderProduct
                            {
                                OrderId = order.Id,
                                ProductId = product.Id,
                                Quantity = cartItem.Quantity
                            };

                            order.OrderProducts.Add(orderProduct);
                        }
                    }
                }

                _context.Orders.Add(order);
                _context.SaveChanges();

                foreach (var cartItem in cart)
                {
                    var orderProduct = _context.OrderProducts.FirstOrDefault(
                        op => op.ProductId == cartItem.Product.Id &&
                        op.OrderId == order.Id
                        );

                    orderProduct.Quantity = cartItem.Quantity;
                }

                _context.SaveChanges();

                Response.Cookies.Delete(CartCookieKey);

                return RedirectToAction("Payment", new { orderId = order.Id });
            }
            return View(model);
        }

        public IActionResult Payment(int orderId)
        {
            var order = _context.Orders.Include(o => o.User)
                                 .Include(o => o.OrderProducts)
                                    .ThenInclude(op => op.Product)
                                 .Include(o => o.Status)
                                 .Include(o => o.ShippingMethod)
                                 .Include(o => o.PaymentMethod)
                                 .FirstOrDefault(o => o.Id == orderId);

            var orderProducts = _context.OrderProducts
                .Where(op => op.OrderId == order.Id);

            ViewBag.OrdersProducts = orderProducts;
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
    }
}
