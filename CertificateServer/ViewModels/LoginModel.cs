using System.ComponentModel.DataAnnotations;

namespace CertificateServer.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Не указан номер телефона")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
