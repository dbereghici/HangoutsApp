using System;
using System.Collections.Generic;
using System.Text;

namespace HangoutsDbLibrary.Model
{
    public class UserChat
    {

        public int UserID { get; set; }
        public User User { get; set; }

        public int ChatID { get; set; }
        public Chat Chat { get; set; }

    }
}
