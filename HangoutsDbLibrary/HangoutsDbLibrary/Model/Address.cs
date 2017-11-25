using System;
using System.Collections.Generic;
using System.Text;

namespace HangoutsDbLibrary.Model
{
    public class Address
    {
        public int ID { get; set; }
        public string Location { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public List<User> Users { get; set; }
        public List<Plan> Plans { get; set; }
    }
}
