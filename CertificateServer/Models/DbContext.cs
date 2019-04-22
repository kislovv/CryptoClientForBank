using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CertificateServer.Models
{
    public class BaseDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Certificate> Certificates { get; set; }

        public BaseDbContext(DbContextOptions<BaseDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
