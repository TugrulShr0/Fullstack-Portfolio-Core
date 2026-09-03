using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class UserMessage
    {
        public int MessageID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }// Mesajın okunup okunmadığını belirtir
        public int UserId { get; set; } 
        public User User { get; set; } // Kullanıcı ile ilişkiyi temsil eder


    }
}
