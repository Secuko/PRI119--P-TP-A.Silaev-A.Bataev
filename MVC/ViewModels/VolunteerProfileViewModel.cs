using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.ViewModels
{
    public class VolunteerProfileViewModel : UserProfileViewModel
    {
        [Required(ErrorMessage = "Не указан номер телефона")]
        [Phone(ErrorMessage = "Введите корректные данные")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Не указан возраст")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Не указан пол")]
        public string Sex { get; set; }
        [Required(ErrorMessage = "Не указана область проживания")]
        public string LivArea { get; set; }
    }
}
