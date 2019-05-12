namespace CertificateServer.Models
{
    using System;

    public class Certificate
    {
        public Guid CertificateId { get; set; }
        public User User { get; set; }
    }
}
