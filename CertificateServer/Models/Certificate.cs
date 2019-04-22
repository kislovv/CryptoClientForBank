using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CertificateServer.Models
{
    public class Certificate
    {
        public Guid CertificateId { get; set; }
        public User User { get; set; }
    }
}
