using System;
using System.Collections.Generic;
using System.Text;

namespace HangoutsDbLibrary.Model
{
    public class Message
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ChatID { get; set; }
        public Chat Chat { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }

    }
}
