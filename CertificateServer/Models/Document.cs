namespace CertificateServer.Models
{
    //TODO : перенести в АПИ
    public enum TypeOfDocument
    {
        xls,
        xlsx,
        doc,
        docx,
        pdf,
        unknown
    }
    public class Document
    {
        public string Name { get; set; }
        public User User { get; set; }
        public string DocumentType { get; set; }
        public byte[] Data { get; set; }
    }
}
