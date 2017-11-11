using System;
using System.Collections.Generic;
using System.Text;

namespace HangoutsDbLibrary.Model
{
    public class PlanUser
    {
        public int PlanID { get; set; }
        public Plan Plan { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }

    }
}
