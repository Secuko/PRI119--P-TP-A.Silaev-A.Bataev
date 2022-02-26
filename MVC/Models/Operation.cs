using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class Operation
    {
        public int Id { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public int? SearchRequestId { get; set; }
        [Required]
        public SearchRequest SearchRequest { get; set; }
        public List<User> Users { get; set; } = new List<User>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
