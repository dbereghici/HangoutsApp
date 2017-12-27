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
            FriendRequestsAccepted = new List<Friendship>();
            FriendRequestsMade = new List<Friendship>();
        }
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age
        {
            get
            {
                int age = DateTime.Today.Year - BirthDate.Year;
                if (BirthDate > DateTime.Today.AddYears(-age)) age--;
                return age;
            }
        }
        public string Email { get; set; }
        public Address Address { get; set; }
        public int AddressID { get; set; }
        public virtual List<UserGroup> UserGroups { get; set; }
        public virtual List<Friendship> FriendRequestsMade { get; set; }
        public virtual List<Friendship> FriendRequestsAccepted { get; set; }
        public virtual List<User> Friends {
            get {
                List<User> friends = new List<User>();
                foreach (var friend in FriendRequestsMade)
                {
                    if (friend.Status.Equals("accepted"))
                        friends.Add(friend.User1);
                }
                foreach (var friend in FriendRequestsAccepted)
                {
                    if (friend.Status.Equals("accepted"))
                        friends.Add(friend.User2);
                }
                 return friends;
            }
        }
        public List<Group> GroupsAdministrated { get; set; }
        public List<PlanUser> PlanUsers { get; set; }
        public List<Message> Messages { get; set; }
        public List<UserChat> UserChats { get; set; }
    }
}
