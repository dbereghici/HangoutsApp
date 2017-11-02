using System;
using System.Collections.Generic;
using System.Text;

namespace HangoutsDbLibrary.Model
{
    public class User
    {
        public User()
        {
            UserGroups = new List<UserGroup>();
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public List<UserGroup> UserGroups { get; set; }
    }
}
