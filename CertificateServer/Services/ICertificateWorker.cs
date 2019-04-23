
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.X509;

namespace CertificateServer.Services
{
    public interface ICertificateWorker
    {
        string SertificatePath { get; set; }
        AsymmetricCipherKeyPair GenerateEcKeyPair(string curveName);
        X509Certificate GenerateCertificate(X509Name issuer, X509Name subject, AsymmetricKeyParameter issuerPrivate, AsymmetricKeyParameter subjectPublic);
        bool ValidateSelfSignedCert(X509Certificate cert, ICipherParameters pubKey);
        AsymmetricCipherKeyPair GenerateRsaKeyPair(int length);
    }
}
