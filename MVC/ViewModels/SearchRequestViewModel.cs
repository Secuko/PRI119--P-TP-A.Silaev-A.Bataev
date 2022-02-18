using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.ViewModels
{
    public class SearchRequestViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Не указано ФИО")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Не указан возраст")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Не указан пол")]
        public string Sex { get; set; }

        [Required(ErrorMessage = "Не указана область пропажи")]
        public string MissArea { get; set; }
        [Required(ErrorMessage = "Не указано время пропажи")]
        public string MissTime { get; set; }
        public string AddInf { get; set; }
    }
}
