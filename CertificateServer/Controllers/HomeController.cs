using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CertificateServer.Models;

namespace CertificateServer.Controllers
{
    public class HomeController : Controller
    {
        readonly BaseDbContext db;
        public HomeController(BaseDbContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View(db.Users.ToList());
        }

        
    }
}
