using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CertificateServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CertificateServer.Controllers
{
    public class HomeController : Controller
    {
        readonly BaseDbContext db;
        public HomeController(BaseDbContext context)
        {
            db = context;
        }


        [Authorize(Roles = "user")]
        public IActionResult Index()
        {
            User user = db.Users.FirstOrDefault(u => u.Phone == User.Identity.Name); 
            return View(user);
        }


    }
}
