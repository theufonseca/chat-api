using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class MessageEntity
    {
        public string MyNick { get; set; }
        public string FriendNick { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
    }
}
