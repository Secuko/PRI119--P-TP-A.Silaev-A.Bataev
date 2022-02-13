using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указано имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указана фамилия")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "Не указан пол")]
        public string Sex { get; set; }

        [Required(ErrorMessage = "Не указан возраст")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Не указан номер телефона")]
        [Phone(ErrorMessage = "Введите корректные данные")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}
