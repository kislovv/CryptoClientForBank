using CertificateServer.Models;
using Microsoft.AspNetCore.Http;

namespace CertificateServer.Services
{
    public interface IFileWorker
    {
        Document FillDocumentModel(IFormFile formFile);
        bool TypeOfDocumentIsValid(string documentType);
    }
}