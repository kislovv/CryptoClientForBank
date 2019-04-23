using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CertificateServer.Models;
using CertificateServer.Services;
using CertificateServer.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CertificateServer.Controllers
{
    public class AccountController : Controller
    {
        private BaseDbContext db;
        private ITwoFactorAuth TwoFactorAuth { get; set; }
        private IAccountManager AccountManager { get; set; }
        public AccountController(BaseDbContext context)
        {
            UserInicialazer.DatabaseInitialaze();
            db = context;
        }

        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
              var user  = await this.AccountManager.ValidateAsync(model, this);
              
                if (user != null)
                {
                    if (user.Role.RoleName.Equals("admin"))
                    {
                        return RedirectToAction("Index", "ChangeEvents");
                    }
                    if (user.Role.RoleName == "client")
                    {
                        var hash = TwoFactorAuth.GetTwoFactorHash();
                        return RedirectToAction("Index", "ChangeEvents");
                    }
                    return RedirectToAction("Office", "PersonalOffice");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }



        //TODO если будет время
        //[HttpGet]
        //public IActionResult Register()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Register(RegisterModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        User user = await db.Users.FirstOrDefaultAsync(u => u.Phone == model);
        //        if (user == null)
        //        {
        //            // добавляем пользователя в бд


        //            user = new User
        //            {
        //                Email = model.Email,
        //                Password = model.Password,
        //                UserGroup = model.Group,
        //                UserNamme = model.FirstName,
        //                UserSecondName = model.SecondName,
        //                UserLastName = model.LastName
        //            };
        //            if (model.LastName == null) user.UserLastName = "";

        //            ProgrammRole userRole = await db.ProgrammRoles.FirstOrDefaultAsync(r => r.Name == "user");
        //            if (userRole != null)
        //                user.ProgrammRole = userRole;

        //            db.Users.Add(user);
        //            await db.SaveChangesAsync();

        //            await Authenticate(user); // аутентификация

        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //            ModelState.AddModelError("", "Некорректные логин и(или) пароль");
        //    }
        //    return View(model);
        //}

        private async Task Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Phone),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.RoleName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        [Route("Account/Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}