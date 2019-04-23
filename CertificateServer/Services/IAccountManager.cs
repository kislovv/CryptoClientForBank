using System.Threading.Tasks;

using CertificateServer.Models;
using CertificateServer.ViewModels;

using Microsoft.AspNetCore.Http;

namespace CertificateServer.Services
{
    public interface IAccountManager
    {
        Task<User> ValidateAsync(LoginModel model, HttpContext context, BaseDbContext baseDbContext);
        Task Authenticate(User user, HttpContext httpContext);
    }
}