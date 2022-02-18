using MVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.ViewModels
{
    public class VolRequestViewModel
    {
        [Required(ErrorMessage = "Не указан возраст")]
        [Phone(ErrorMessage = "Введите номер телефона")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Не указан возраст")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Не указан пол")]
        public string Sex { get; set; }

        [Required(ErrorMessage = "Не указана область проживания")]
        public string LivArea { get; set; }
    }
}
