using CertificateServer.Models;

using System;
using System.Linq;

namespace CertificateServer.Services
{
    public class UserInicialazer
    {
        
        public static void DatabaseInitialaze(BaseDbContext context)
        {
            if (!context.Roles.Any() || !context.Users.Any())
            {
                var adminRole = new Role { RoleId = Guid.NewGuid(), RoleName = "admin" };
                var userRole = new Role { RoleId = Guid.NewGuid(), RoleName = "user" };
                context.Roles.Add(adminRole);
                context.Roles.Add(userRole);
                context.Users.AddRange(
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
                context.SaveChanges();
            }
        }
    }
}
