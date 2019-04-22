using CertificateServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CertificateServer.Services
{
    public class UserInicialazer
    {
        private static BaseDbContext DataBaseContext { get; set; }
        public static void DatabaseInitialaze()
        {
            if (!DataBaseContext.Roles.Any() && !DataBaseContext.Users.Any())
            {
                var adminRole = new Role { RoleName = "admin" };
                var userRole = new Role { RoleName = "user" };
                DataBaseContext.Roles.Add(adminRole);
                DataBaseContext.Roles.Add(userRole);
                DataBaseContext.Users.AddRange(
                    new User
                    {
                        Name = "Kirill",
                        Password = "123456",
                        Phone = "+79050086690",
                        Role = userRole,
                        Sername = "Kislov",
                        UserId = Guid.NewGuid()
                    },
                    new User
                    {
                        Name = "Alex",
                        Password = "654321",
                        Phone = "+7907899009",
                        Role = adminRole,
                        Sername = "Kirichek",
                        UserId = Guid.NewGuid()
                    },
                    new User
                    {
                        Name = "Ura",
                        Password = "123321",
                        Phone = "+790340043690",
                        Role = userRole,
                        Sername = "Mislov",
                        UserId = Guid.NewGuid()
                    });
                DataBaseContext.SaveChanges();
            }
        }
    }
}
