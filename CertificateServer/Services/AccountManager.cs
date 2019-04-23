using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using CertificateServer.Models;
using CertificateServer.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CertificateServer.Services
{
    public class AccountManager : IAccountManager
    {
        //TODO Сюда вынести авторизацию и аутентификацию, контроллеры нужно разгрузить

        public BaseDbContext DataContext { get; set; }

        public async Task<User> ValidateAsync(LoginModel model, Controller controller)
        {
            User user = await DataContext.Users
                   .Include(u => u.Role)
                   .FirstOrDefaultAsync(u => u.Phone == model.Phone && u.Password == model.Password);

            if (user != null)
            {
                await Authenticate(user, controller); // аутентификация
                return user;
            }
            return null;
        }

        private async Task Authenticate(User user, Controller controller)
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
            await controller.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
