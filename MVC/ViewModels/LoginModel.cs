using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Не указан номер телефона")]
        [Phone(ErrorMessage = "Введите корректные данные")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
