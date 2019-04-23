using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using CertificateServer.Models;
using CertificateServer.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CertificateServer.Services
{
    public class AccountManager : IAccountManager
    {
        //TODO Сюда вынести авторизацию и аутентификацию, контроллеры нужно разгрузить

        public async Task<User> ValidateAsync(LoginModel model, HttpContext httpContext , BaseDbContext DataContext)
        {
            User user = await DataContext.Users
                   .Include(u => u.Role)
                   .FirstOrDefaultAsync(u => u.Phone == model.Phone && u.Password == model.Password);

            if (user != null)
            {
               
                return user;
            }
            return null;
        }

        public async Task Authenticate(User user, HttpContext httpContext)
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
            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
