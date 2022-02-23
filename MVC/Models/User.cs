using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "Не указано имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указана фамилия")]
        public string SurName { get; set; }
        public SearchRequest SearchRequest { get; set; }
        public VolRequest VolRequest { get; set; }
        public List<Operation> Operations { get; set; } = new List<Operation>();  
        public User() { }
    }
}
