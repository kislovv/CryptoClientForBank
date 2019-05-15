using CertificateServer.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CertificateServer.Services
{
    public class FileWorker : IFileWorker
    {
        public Document FillDocumentModel(IFormFile formFile, string extencion, User user)
        {
            using (var formDataFile = formFile.OpenReadStream())
            {
                var document = new Document
                {
                    Data = ReadFully(formDataFile),
                    Name = formFile.Name,
                    DocumentType = extencion,
                    User = user
                };
                return document;
            }
        }

        public bool TypeOfDocumentIsValid(string documentType)
        {
            var docType = TypeOfDocument.unknown;
            switch (documentType)
            {
                case ".doc":
                case ".txt":
                    docType = TypeOfDocument.doc;
                    break;
                case ".docx":
                    docType = TypeOfDocument.docx;
                    break;
                case ".xls":
                    docType = TypeOfDocument.xls;
                    break;
                case ".xlsx":
                    docType = TypeOfDocument.xlsx;
                    break;
                case ".pdf":
                    docType = TypeOfDocument.pdf;
                    break;               
            }
            return docType != TypeOfDocument.unknown;
        }

        /// <summary>
        /// Reads data from a stream until the end is reached. The
        /// data is returned as a byte array. An IOException is
        /// thrown if any of the underlying IO calls fail.
        /// </summary>
        /// <param name="stream">The stream to read data from</param>
        private static byte[] ReadFully(Stream stream)
        {
            byte[] buffer = new byte[32768];
            using (MemoryStream ms = new MemoryStream())
            {
                while (true)
                {
                    int read = stream.Read(buffer, 0, buffer.Length);
                    if (read <= 0)
                        return ms.ToArray();
                    ms.Write(buffer, 0, read);
                }
            }
        }
    }
}
