namespace CertificateServer.Models
{
    public enum TypeOfDocument
    {
        xls,
        xlsx,
        doc,
        docx,
        pdf
    }
    public class Document
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public User User { get; set; }
        public TypeOfDocument DocumentType { get; set; }
        public byte[] Data { get; set; }
    }
}
