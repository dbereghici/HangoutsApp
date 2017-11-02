using System;
using System.Collections.Generic;
using System.Text;

namespace HangoutsDbLibrary.Model
{
    public class Activity
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public Group Group { get; set; }
    }
}
