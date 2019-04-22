﻿using System;

using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

namespace CertificateServer.Services
{
    public class CertificateWorker : ICertificateWorker
    {
        public string SertificatePath { get; set; }
        public SecureRandom SecureRandom { get; } = new SecureRandom();

        public AsymmetricCipherKeyPair GenerateEcKeyPair(string curveName)
        {
            var ecParam = SecNamedCurves.GetByName(curveName);
            var ecDomain = new ECDomainParameters(ecParam.Curve, ecParam.G, ecParam.N);
            var keygenParam = new ECKeyGenerationParameters(ecDomain, SecureRandom);

            var keyGenerator = new ECKeyPairGenerator();
            keyGenerator.Init(keygenParam);
            return keyGenerator.GenerateKeyPair();
        }

        public X509Certificate GenerateCertificate(
        X509Name issuer, X509Name subject,
        AsymmetricKeyParameter issuerPrivate,
        AsymmetricKeyParameter subjectPublic)
        {
            ISignatureFactory signatureFactory;
            if (issuerPrivate is ECPrivateKeyParameters)
            {
                signatureFactory = new Asn1SignatureFactory(
                    X9ObjectIdentifiers.ECDsaWithSha256.ToString(),
                    issuerPrivate);
            }
            else
            {
                signatureFactory = new Asn1SignatureFactory(
                    PkcsObjectIdentifiers.Sha256WithRsaEncryption.ToString(),
                    issuerPrivate);
            }

            var certGenerator = new X509V3CertificateGenerator();
            certGenerator.SetIssuerDN(issuer);
            certGenerator.SetSubjectDN(subject);
            certGenerator.SetSerialNumber(BigInteger.ValueOf(1));
            certGenerator.SetNotAfter(DateTime.UtcNow.AddYears(5));
            certGenerator.SetNotBefore(DateTime.UtcNow);
            certGenerator.SetPublicKey(subjectPublic);
            return certGenerator.Generate(signatureFactory);
        }


        public bool ValidateSelfSignedCert(X509Certificate cert, ICipherParameters pubKey)
        {
            cert.CheckValidity(DateTime.UtcNow);
            var tbsCert = cert.GetTbsCertificate();
            var sig = cert.GetSignature();

            var signer = SignerUtilities.GetSigner(cert.SigAlgName);
            signer.Init(false, pubKey);
            signer.BlockUpdate(tbsCert, 0, tbsCert.Length);
            return signer.VerifySignature(sig);
        }


        public  AsymmetricCipherKeyPair GenerateRsaKeyPair(int length)
        {
            var keygenParam = new KeyGenerationParameters(SecureRandom, length);

            var keyGenerator = new RsaKeyPairGenerator();
            keyGenerator.Init(keygenParam);
            return keyGenerator.GenerateKeyPair();
        }
    }
}
