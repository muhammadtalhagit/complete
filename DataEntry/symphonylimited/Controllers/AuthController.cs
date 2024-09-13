using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;

using symphonylimited.Models;

namespace symphonylimited.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AuthController : Controller
    {
        private readonly SymphonyContext db;

        public AuthController(SymphonyContext _db)
        {
            db = _db;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Login(User user)
        {
            bool IsAuthenticated = false;

            ClaimsIdentity identity = null;
            string controller = "";
            var checkUser = db.Users.FirstOrDefault(u => u.Email == user.Email);
            if (checkUser != null)
            {
             
                if (user.Password == checkUser.Password)
                {
                    identity = new ClaimsIdentity(new[]
                 {
                    new Claim(ClaimTypes.Name ,checkUser.Username),
                    new Claim(ClaimTypes.Role ,"Admin"),

                }
                , CookieAuthenticationDefaults.AuthenticationScheme);
                    IsAuthenticated = true;
                    controller = "Admin";

                    HttpContext.Session.SetInt32("UserID", checkUser.Id);
                    HttpContext.Session.SetString("UserEmail", checkUser.Email);

                }
              
                else
                {
                    ViewBag.msg = "Invalid Credentials";
                    return View();
                }


                if (IsAuthenticated)
                {
                    var principal = new ClaimsPrincipal(identity);

                    var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", controller);
                }
                else
                {
                    ViewBag.msg = "Login Failed";
                    return View();

                }
            }

            else
            {
                ViewBag.msg = "Invalid User";
                return View();
            }



        }




        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserID");
            HttpContext.Session.Remove("UserEmail");

            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");
        }

    }
}


