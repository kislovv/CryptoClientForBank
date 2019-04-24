using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using CertificateServer.Models;
using CertificateServer.Services;
using CertificateServer.ViewModels;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;


namespace CertificateServer.Controllers
{
    public class AccountController : Controller
    {
        private BaseDbContext db;
        private ITwoFactorAuth TwoFactorAuth { get; set; }
        private IAccountManager AccountManager { get; set; }
        public AccountController(BaseDbContext context, IAccountManager accountManager , ITwoFactorAuth  twoFactorAuth)
        {
            UserInicialazer.DatabaseInitialaze(context);
            db = context;
            AccountManager = accountManager;
            TwoFactorAuth = twoFactorAuth;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
              var user  = AccountManager.ValidateAsync(model, HttpContext, db);
              
                if (user?.Result != null)
                {
                    AccountManager.Authenticate(user.Result, HttpContext);
                    
                    if (user.Result.Role.RoleName == "user")
                    {
                        var hash = TwoFactorAuth.GetTwoFactorHash();
                        return RedirectToAction("Office", "PersonalOffice");
                    }                  
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        [Route("Account/Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
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
    }
}