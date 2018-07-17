using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsrAccount.Data;
using UsrAccount.Models;

namespace UsrAccount.Controllers
{
    public class LoginController : Controller
    {

        private readonly AccountContext _context;

        public LoginController(AccountContext context)
        {
            _context = context;
        }

        public IActionResult Forbidden(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpGet]
        public IActionResult UserLogin(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserLogin([Bind("UserId","Password")] UserIdentity user, string returnUrl = null)
        {

            if (ModelState.IsValid)
            {

                var userIdentity = _context.UserIdentities.AsNoTracking().SingleOrDefault(u => u.UserId == user.UserId);

                if (userIdentity != null && user.Password == userIdentity.Password)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim("NicName", userIdentity.NicName),
                        new Claim(ClaimTypes.Name, userIdentity.UserId),
                        new Claim(ClaimTypes.Role, userIdentity.Role),
                        new Claim("UserGroup", userIdentity.UserGroup)

                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)),
                        new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTime.UtcNow.AddMinutes(1)
                        });

                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect("/Home/Index");

                    }


                    return LocalRedirect(returnUrl);


                }
                

            }

            ViewData["ReturnUrl"] = returnUrl;
            //TempData["UserLoginFailed"] = "아이디 또는 패스워드 확인";

            ViewData["ErrMsg"] = "아이디 또는 패스워드 확인";
            return View();


        }


        [HttpPost]
        public async Task<IActionResult> UserLogout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Home/Index");
        }




    }

}