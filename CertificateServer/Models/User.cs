namespace CertificateServer.Models
{
    using System;

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
