using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CertificateServer.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Sername { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
