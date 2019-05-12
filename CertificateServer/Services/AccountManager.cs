namespace CertificateServer.Services
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using CertificateServer.Models;
    using CertificateServer.ViewModels;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    public class AccountManager : IAccountManager
    {
        public async Task<User> ValidateAsync(LoginModel model, HttpContext httpContext , BaseDbContext DataContext)
        {
            var user = await DataContext.Users
                   .Include(u => u.Role)
                   .FirstOrDefaultAsync(u => u.Phone == model.Phone && u.Password == model.Password);

            return user ?? null;
        }

        public async Task Authenticate(User user, HttpContext httpContext)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Phone),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.RoleName)
            };
            // создаем объект ClaimsIdentity
            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
