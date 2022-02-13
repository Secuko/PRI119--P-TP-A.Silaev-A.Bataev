using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class Volunteer : User
    {
        [Required(ErrorMessage = "Не указан пол")]
        public string Sex { get; set; }
        [Required(ErrorMessage = "Не указан возраст")]
        public int Age { get; set; }
        public string LivArea { get; set; }

        public Volunteer(string name, string surName, string sex, int age, string livArea)
            : base(name, surName)
        {
            Sex = sex;
            Age = age;
            LivArea = livArea;
        }
    }
}
