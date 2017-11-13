using System;
using System.Collections.Generic;
using System.Text;

namespace HangoutsDbLibrary.Model
{
    public class Group
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public User Admin { get; set; }
        public int AdminID { get; set; }
        public List<UserGroup> UserGroups { get; set; }
        public List<Activity> Activities { get; set; }
        public List<Plan> Plans { get; set; }
    }
}
