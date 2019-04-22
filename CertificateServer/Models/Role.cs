using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CertificateServer.Models
{
    public class Role
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();

        public Role()
        {
            Users = new List<User>();
        }
    }
}
