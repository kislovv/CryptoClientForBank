using CertificateServer.Models;
using Microsoft.AspNetCore.Http;

namespace CertificateServer.Services
{
    public interface IFileWorker
    {
        Document FillDocumentModel(IFormFile formFile, string extencion, User user);
        bool TypeOfDocumentIsValid(string documentType);
    }
}