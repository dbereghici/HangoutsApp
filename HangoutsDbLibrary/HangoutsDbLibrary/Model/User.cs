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
            Friends1 = new List<Friendship>();
            Friends2 = new List<Friendship>();
        }
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }

        public virtual List<UserGroup> UserGroups { get; set; }
        public List<Friendship> Friends1 { get; set; }
        public List<Friendship> Friends2 { get; set; }
        public List<User> Friends {
            get {
                List<User> friends = new List<User>();
                foreach (var friend in Friends1)
                {
                    friends.Add(friend.User1);
                }
                foreach (var friend in Friends2)
                {
                    friends.Add(friend.User2);
                }
                return friends;
            }
            }
        public List<Group> GroupsAdministrated { get; set; }
        public List<PlanUser> PlanUsers { get; set; }
        public List<Message> Messages { get; set; }


        //public int GroupAdministratedID { get; set; }
        //public Group GroupAdministrated { get; set; }
    }
}
