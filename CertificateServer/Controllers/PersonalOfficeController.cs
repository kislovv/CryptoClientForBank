namespace CertificateServer.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using CertificateServer.Models;
    using CertificateServer.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class PersonalOfficeController : Controller
    {

        readonly BaseDbContext db;
        IFileWorker fileWorker;
        public PersonalOfficeController(BaseDbContext context, IFileWorker fileWorker)
        {
            db = context;
            this.fileWorker = fileWorker;
        }
        [Authorize(Roles = "user")]
        public IActionResult Office()
        {
            var user = db.Users.FirstOrDefault(u => u.Phone == User.Identity.Name);
            return View(user ?? new User());
        }

        [HttpPost]
        public IActionResult AddFile(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                var user = db.Users.FirstOrDefault(u => u.Phone == User.Identity.Name);
                // путь к папке Files
                string path = "/Files/" + uploadedFile.FileName;
                var extention = Path.GetExtension(path);
                if (fileWorker.TypeOfDocumentIsValid(extention))
                {
                    var document = fileWorker.FillDocumentModel(uploadedFile, extention, user);
                    //TODO Отправить запрос в апи с нужными параметрами если все валидно, пока мок, еще добавить валидацию данных
                    return RedirectToAction("Office");
                }
            }

            return RedirectToAction("Index");
        }
    }
}