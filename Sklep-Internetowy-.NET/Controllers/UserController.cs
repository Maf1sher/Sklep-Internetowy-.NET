using Microsoft.AspNetCore.Mvc;
using Sklep_Internetowy_.NET.Models.Entity;
using Sklep_Internetowy_.NET.Models.ViewModel;
using System.Linq;
using test_do_projektu.Data;

namespace Sklep_Internetowy_.NET.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var user = GetCurrentUser();

            if (user == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var model = new UserProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Country = user.Country,
                City = user.City,
                Address = user.Address,
                Zip = user.Zip
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult EditProfile()
        {
            var user = GetCurrentUser();

            if (user == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var model = new EditProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Country = user.Country,
                City = user.City,
                Address = user.Address,
                Zip = user.Zip
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult EditProfile(EditProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = GetCurrentUser();

            if (user == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Country = model.Country;
            user.City = model.City;
            user.Address = model.Address;
            user.Zip = model.Zip;

            _context.Users.Update(user);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Profile updated successfully!";
            return RedirectToAction("Profile");
        }

        [HttpGet]
        public IActionResult Orders()
        {
            var user = GetCurrentUser();

            if (user == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var orders = _context.Orders
                .Where(o => o.UserId == user.Id)
                .Select(o => new UserOrdersViewModel
                {
                    Id = o.Id,
                    OrderDate = o.CreatedData,
                    Status = o.Status.StatusName,
                    ShippingMethod = o.ShippingMethod.ShippingName,
                    PaymentMethod = o.PaymentMethod.MethodName,
                    OrderItems = o.OrderProducts.Select(oi => new UserOrderItemsViewModel
                    {
                        ProductName = oi.Product.Name,
                        Quantity = oi.Quantity,
                        Price = oi.Product.Price
                    }).ToList()
                })
                .ToList();

            return View(orders);
        }

        private User GetCurrentUser()
        {
            var userEmail = User.Identity?.Name;

            if (string.IsNullOrEmpty(userEmail))
            {
                return null;
            }

            return _context.Users.FirstOrDefault(u => u.Email == userEmail);
        }
    }
}
