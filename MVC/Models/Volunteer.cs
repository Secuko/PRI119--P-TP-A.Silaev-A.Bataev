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
        public string Phone { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public string LivArea { get; set; }
    }
}
