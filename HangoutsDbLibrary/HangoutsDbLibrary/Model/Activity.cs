using System;
using System.Collections.Generic;
using System.Text;

namespace HangoutsDbLibrary.Model
{
    public class Activity
    {
        public int ID { get; set; }
        public Group Group { get; set; }
        public int GroupID { get; set; }

    }
}
