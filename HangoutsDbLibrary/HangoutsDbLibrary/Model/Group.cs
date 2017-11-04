using System;
using System.Collections.Generic;
using System.Text;

namespace HangoutsDbLibrary.Model
{
    public class Group
    {
        public Group()
        {
            UserGroups = new List<UserGroup>();
        }
        public string GroupNr { get; set; }

        public Chat Chat { get; set; }

        public GroupAdmin Admin { get; set; }

        public List<Activity> Activities { get; set; }

        public List<UserGroup> UserGroups { get; set; }

        
    }
}
