using System;
using System.Collections.Generic;
using System.Text;

namespace HangoutsDbLibrary.Model
{
    public class Chat
    {
        public int ID { get; set; }
        public DateTime CreatedAt { get; set; }
        public Plan Plan { get; set; }
        public List<Message> Messages { get; set; }
        public Friendship Friendship { get; set; }
        public List<UserChat> UserChats { get; set; }
    }
}
