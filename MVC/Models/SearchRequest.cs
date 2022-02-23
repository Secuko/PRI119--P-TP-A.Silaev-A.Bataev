using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class SearchRequest
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Не указан возраст")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Не указан пол")]
        public string Sex { get; set; }

        [Required(ErrorMessage = "Не указана область пропажи")]
        public string MissArea { get; set; }
        [Required]
        public string MissTime { get; set; }
        public string AddInf { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string UserId { get; set; }
        public User User { get; set; }

        public SearchRequest() { }
    }
}
