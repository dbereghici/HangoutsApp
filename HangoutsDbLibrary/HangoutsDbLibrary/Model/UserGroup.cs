using System;
using System.Collections.Generic;
using System.Text;

namespace HangoutsDbLibrary.Model
{
    public class UserGroup
    {
        public int UserID { get; set; }
        public virtual User User { get; set; }

        public int GroupID { get; set; }
        public virtual Group Group { get; set; }
    }
}
