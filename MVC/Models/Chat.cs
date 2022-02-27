using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public SearchRequest SearchRequest { get; set; }
        public int SearchRequestId { get; set; }
        public List<Message> Messages { get; set; } = new List<Message>();
        public List<User> Users { get; set; } = new List<User>();
    }
}
