using System.Threading.Tasks;
using CertificateServer.Models;
using CertificateServer.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CertificateServer.Services
{
    public interface IAccountManager
    {
        BaseDbContext DataContext { get; set; }

        Task<User> ValidateAsync(LoginModel model, Controller controller);
    }
}