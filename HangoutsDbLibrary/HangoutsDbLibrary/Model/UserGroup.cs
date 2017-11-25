using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HangoutsDbLibrary.Model
{
    public class UserGroup
    {
        public int UserID { get; set; }
        public  User User { get; set; }

        public int GroupID { get; set; }
        public  Group Group { get; set; }

        public string Status { get; set; }
    }
}
