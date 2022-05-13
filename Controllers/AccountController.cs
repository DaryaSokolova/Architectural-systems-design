using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication7.ViewModels; // пространство имен моделей RegisterModel и LoginModel
using WebApplication7.Models; // пространство имен UserContext и класса User
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using DAL;
using System.Security.Cryptography;
using System.Text;

namespace WebApplication7.Controllers
{
    public class AccountController : Controller
    {
        private bakaContext db;

        public AccountController(bakaContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        private static SHA512 hashAlgo = SHA512.Create();

        private static byte[] GetStringHash(string s)
        {
            if (s == null)
                return null;
            byte[] hash = hashAlgo.ComputeHash(Encoding.Unicode.GetBytes(s));
            return hash;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Name == model.Name && u.Password == GetStringHash(model.Password));
                if (user != null)
                {
                    await Authenticate(model.Name); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Name == model.Name);
                if (user == null)
                {
                    // добавляем пользователя в бд
                    db.Users.Add(new User { Name = model.Name, Password = GetStringHash(model.Password) });
                    await db.SaveChangesAsync();

                    await Authenticate(model.Name); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
