using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class Volunteer : User
    {
        public string LivArea { get; set; }
        public string Email { get; set; }

        public Volunteer(string name, string surName, string sex, int age, int id, string password, string phone, string livArea, string email)
            : base(name, surName, sex, age, id, password, phone)
        {
            LivArea = livArea;
            Email = email;
        }
    }
}
