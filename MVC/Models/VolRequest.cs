using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class VolRequest
    {
        public int Id { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Sex { get; set; }
        [Required]
        public string LivArea { get; set; }
        public string Status { get; set; }
        [Required]
        public string UserId { get; set; }
        public User User { get; set; }

        public VolRequest() { }
    }
}
