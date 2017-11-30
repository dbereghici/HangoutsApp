using System;
using System.Collections.Generic;
using System.Text;

namespace HangoutsDbLibrary.Model
{
    public class Friendship
    {
        public int UserID1 { get; set; }
        public User User1 { get; set; }

        public int UserID2 { get; set; }
        public User User2 { get; set; }

        public string Status { get; set; }
        public Chat Chat { get; set; }
        public int ChatID { get; set; }
    }
}
