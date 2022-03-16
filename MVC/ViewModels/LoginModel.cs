using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Не указан номер телефона")]
        [EmailAddress(ErrorMessage = "Введите корректные данные")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }

    }
}
