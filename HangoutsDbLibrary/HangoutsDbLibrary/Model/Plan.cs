using System;
using System.Collections.Generic;
using System.Text;

namespace HangoutsDbLibrary.Model
{
    public class Plan
    {
        public int ID { get; set; }
        public int ChatID { get; set; }
        public Chat Chat { get; set; }
        public int GroupID { get; set; }
        public Group Group { get; set; }
        public DateTime CreatedAt { get; set; }
        public int LifeTime { get; set; }
        //public int createdByUserID { get; set; }
        //public User createdByUser { get; set; }
        public List<PlanUser> PlanUsers { get; set; }
    }
}
