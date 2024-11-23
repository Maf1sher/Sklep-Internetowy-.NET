using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy_.NET.Data;
using Sklep_Internetowy_.NET.Models.ViewModel;
using Sklep_Internetowy_.NET.Models.Entity;
using System.Security.Claims;
using test_do_projektu.Data;

namespace Sklep_Internetowy_.NET.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext appDbContext)
        {
            this._context = appDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserRequest request)
        {
            if(ModelState.IsValid)
            {
                User user = new User();
                user.Email = request.Email;
                user.Password = SecurePasswordHasher.Hash(request.Password);
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.Country = request.Country;
                user.Zip = request.Zip;
                user.City = request.City;
                user.Address = request.Address;
                user.Role = "USER";

                try
                {
                    _context.Users.Add(user);
                    _context.SaveChanges();

                    ModelState.Clear();
                    return RedirectToAction(actionName: "Login", controllerName: "Auth");
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "This email is busy");
                }
                
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginUserRequest request)
        {
            if (ModelState.IsValid)
            {
                //var user = _context.Users.Where(x =>
                //x.Email == request.Email && x.Password == request.Password)
                //    .FirstOrDefault();

                var user = _context.Users.Where(x =>
                x.Email == request.Email)
                    .FirstOrDefault();
                if (user != null)
                {
                    if (!SecurePasswordHasher.Verify(request.Password, user.Password))
                    {
                        ModelState.AddModelError("", "Email or Password is not correct");
                        return View();
                    }

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim("Firstname", user.FirstName),
                        new Claim(ClaimTypes.Role, user.Role)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return RedirectToAction(actionName: "Index", controllerName: "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Email or Password is not correct");
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }

        [Authorize(Roles = "USER")]
        public IActionResult SecurePage()
        {
            ViewBag.Name = HttpContext.User.Identity.Name; 
            return View();
        }

        [Authorize(Roles = "ADMIN")]
        public IActionResult AdminSecurePage()
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View();
        }
    }
}
