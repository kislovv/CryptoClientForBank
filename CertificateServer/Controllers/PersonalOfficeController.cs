using System.Linq;

using CertificateServer.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CertificateServer.Controllers
{
    public class PersonalOfficeController : Controller
    {

        readonly BaseDbContext db;
        public PersonalOfficeController(BaseDbContext context)
        {
            db = context;
        }


        [Authorize(Roles = "user")]
        public IActionResult Office()
        {
            User user = db.Users.FirstOrDefault(u => u.Phone == User.Identity.Name);
            return View(user);
        }
    }
}