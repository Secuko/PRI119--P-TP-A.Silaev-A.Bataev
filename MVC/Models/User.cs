using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class User : IdentityUser
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Не указано имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Не указана фамилия")]
        public string SurName { get; set; }
        [Required(ErrorMessage = "Не указан пол")]
        public string Sex { get; set; }
        [Required(ErrorMessage = "Не указан возраст")]
        public int Age { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Не указан номер телефона")]
        [Phone(ErrorMessage = "Введите корректные данные")]
        public string Phone { get; set; }

        public User() { }

        public User(string name, string surName, string sex, int age, int id, string password, string phone)
        {
            Id = id;
            Name = name;
            SurName = surName;
            Sex = sex;
            Age = age;
            Password = password;
            Phone = phone;
        }
    }
}
