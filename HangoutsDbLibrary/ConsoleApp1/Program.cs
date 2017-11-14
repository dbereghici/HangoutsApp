using HangoutsDbLibrary.Data;
using HangoutsDbLibrary.Model;
using HangoutsDbLibrary.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        { 

            using (var db = new HangoutsContext(new Microsoft.EntityFrameworkCore.DbContextOptions<HangoutsContext>()))
            {
                
                DbInitializer.Initialize(db);

                using (var uow = new UnitOfWork())
                {
                    var userRepository = uow.GetRepository<User>();
                    var friendshipRepository = uow.GetRepository<Friendship>();
                    var usergroupRepository = uow.GetRepository<UserGroup>();

                    ConsoleKeyInfo input;
                    do
                    {
                        Console.WriteLine("Menu:");
                        Console.WriteLine("0 - Exit");
                        Console.WriteLine("1 - User Management");
                        Console.WriteLine("2 - Group Management");
                        Console.WriteLine("3 - Friendship Management");
                        input = Console.ReadKey();
                        Console.WriteLine();
                        switch (input.Key)
                        {
                            case ConsoleKey.D1:
                                UserManagement();                               
                                break;
                            case ConsoleKey.D2:
                                GroupManagement();
                                break;
                            case ConsoleKey.D3:
                                FriendshipManagement();
                                break;
                        }

                    }
                    while (input.Key != ConsoleKey.D0);

                }
            }
        }
        public static void UserManagement()
        {
            using (var db = new HangoutsContext(new Microsoft.EntityFrameworkCore.DbContextOptions<HangoutsContext>()))
            {
                DbInitializer.Initialize(db);

                using (var uow = new UnitOfWork())
                {
                    var userRepository = uow.GetRepository<User>();
                    ConsoleKeyInfo input;
                    List<User> users;
                    do
                    {
                        Console.WriteLine("     Menu user management:");
                        Console.WriteLine("     0 - Exit");
                        Console.WriteLine("     1 - Add new user");
                        Console.WriteLine("     2 - Delete an existent user");
                        Console.WriteLine("     3 - Edit an existent user");
                        Console.WriteLine("     4 - View all users");

                        input = Console.ReadKey();
                        Console.WriteLine();

                        switch (input.Key)
                        {
                            case ConsoleKey.D1:
                                User user = AddNewUser();
                                userRepository.Insert(user);
                                uow.SaveChanges();
                                break;
                            case ConsoleKey.D2:
                                users = userRepository.GetAll().ToList();
                                foreach(var u in users)
                                {
                                    Console.WriteLine("      ID: " + u.ID + "   username:" + u.Username);
                                }
                                Console.WriteLine("      Please choose an ID:");
                                string idString = Console.ReadLine();
                                int id;
                                while(!int.TryParse(idString, out id))
                                {
                                    Console.WriteLine("      Incorrect input. Please try again : ");
                                    idString = Console.ReadLine();
                                }
                                bool isIdValid = false;
                                foreach(var u in users)
                                {
                                    if (u.ID == id) { 
                                        isIdValid = true;
                                        break;
                                    }
                                }
                                if (isIdValid)
                                {
                                    User userToBeDeleted = userRepository.GetByID(id);
                                    userRepository.Delete(userToBeDeleted);
                                    uow.SaveChanges();
                                    Console.WriteLine("The selected user was deleted successfully!");
                                }
                                else
                                {
                                    Console.WriteLine("There is no User with such ID!");
                                }
                                break;
                            case ConsoleKey.D3:
                                users = userRepository.GetAll().ToList();
                                foreach (var u in users)
                                {
                                    Console.WriteLine("      ID: " + u.ID + "   username:" + u.Username);
                                }
                                Console.WriteLine("      Please choose an ID:");
                                idString = Console.ReadLine();
                                while (!int.TryParse(idString, out id))
                                {
                                    Console.WriteLine("      Incorrect input. Please try again : ");
                                    idString = Console.ReadLine();
                                }
                                isIdValid = false;
                                foreach (var u in users)
                                {
                                    if (u.ID == id)
                                    {
                                        isIdValid = true;
                                        break;
                                    }
                                }
                                if (isIdValid)
                                {
                                    User userToBeEdited = userRepository.GetByID(id);

                                    User newUser = AddNewUser();
                                    userToBeEdited.Username = newUser.Username;
                                    userToBeEdited.Password = newUser.Password;
                                    userToBeEdited.Email = newUser.Email;
                                    userToBeEdited.FirstName = newUser.FirstName;
                                    userToBeEdited.LastName = newUser.LastName;
                                    userToBeEdited.Location = newUser.Location;
                                    userToBeEdited.Age = newUser.Age;


                                    userRepository.Edit(userToBeEdited);
                                    uow.SaveChanges();
                                    Console.WriteLine("The selected user was edited successfully!");
                                }
                                else
                                {
                                    Console.WriteLine("There is no User with such ID!");
                                }
                                break;
                            case ConsoleKey.D4:
                                users = userRepository.GetAll().ToList();
                                foreach(var u in users)
                                {
                                    Console.WriteLine("      ID -> " + u.ID);
                                    Console.WriteLine("         Username -> " + u.Username);
                                    Console.WriteLine("         Password -> " + u.Password);
                                    Console.WriteLine("         Email -> " + u.Email);
                                    Console.WriteLine("         First name -> " + u.FirstName);
                                    Console.WriteLine("         Last name -> " + u.LastName);
                                    Console.WriteLine("         Location -> " + u.Location);
                                    Console.WriteLine("         Age -> " + u.Age);
                                    Console.WriteLine();
                                }
                                break;
                        }
                    } while (input.Key != ConsoleKey.D0);
                }
            }
        }

        public static User AddNewUser()
        {
            Console.WriteLine("         Please introduce user data");
            Console.Write("         (required) username = ");
            string username = Console.ReadLine();
            while (username.Length == 0)
            {
                Console.WriteLine("         Username is required!");
                Console.Write("         (required) username = ");
                username = Console.ReadLine();
            }
            Console.WriteLine();
            Console.Write("         (required) password = ");
            string password = Console.ReadLine();
            while (password.Length == 0)
            {
                Console.WriteLine("         Password is required!");
                Console.Write("         (required) password = ");
                password = Console.ReadLine();
            }
            Console.WriteLine();
            Console.Write("         (required) email = ");
            string email = Console.ReadLine();
            while (email.Length == 0)
            {
                Console.WriteLine("         Email is required!");
                Console.Write("         (required) email = ");
                email = Console.ReadLine();
            }
            Console.WriteLine();
            Console.Write("         First Name = ");
            string firstName = Console.ReadLine();
            Console.WriteLine();
            Console.Write("         Last Name = ");
            string lastName = Console.ReadLine();
            Console.WriteLine();
            Console.Write("         Location = ");
            string location = Console.ReadLine();
            Console.WriteLine();
            Console.Write("         Age = ");
            string ageString = Console.ReadLine();
            int age = 0;
            if (ageString.Length != 0)
            {
                while (!int.TryParse(ageString, out age))
                {
                    Console.WriteLine("Age input must be an integer!");
                    Console.Write("         Age = ");
                    ageString = Console.ReadLine();
                }
            }
            User user = new User
            {
                Username = username,
                Password = password,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Location = location,
                Age = age
            };
            return user;
        }
        
        public static void GroupManagement()
        {
            using (var db = new HangoutsContext(new Microsoft.EntityFrameworkCore.DbContextOptions<HangoutsContext>()))
            {
                DbInitializer.Initialize(db);

                using (var uow = new UnitOfWork())
                {
                    var groupRepository = uow.GetRepository<Group>();
                    var userRepository = uow.GetRepository<User>();
                    var usergroupRepository = uow.GetRepository<UserGroup>();
                    ConsoleKeyInfo input;
                    List<Group> groups;
                    do
                    {
                        Console.WriteLine("     Menu group management:");
                        Console.WriteLine("     0 - Exit");
                        Console.WriteLine("     1 - Add new group");
                        Console.WriteLine("     2 - Delete an existent group");
                        Console.WriteLine("     3 - Edit an existent group");
                        Console.WriteLine("     4 - View all groups");
                        Console.WriteLine("     5 - Add a user to a group");
                        Console.WriteLine("     6 - View all users from a specific group");

                        input = Console.ReadKey();
                        Console.WriteLine();

                        switch (input.Key)
                        {
                            case ConsoleKey.D1:
                                var users = userRepository.GetAll().ToList();
                                if (users.Count() == 0)
                                {
                                    Console.WriteLine("         A group is created by an Admin. There aren't any users in the database " +
                                        "to create the Group.");
                                    break;
                                }
                                Console.WriteLine("         Please introduce group data");
                                Console.Write("         (required) name = ");
                                string name = Console.ReadLine();
                                while (name.Length == 0)
                                {
                                    Console.WriteLine("         Group name is required!");
                                    Console.Write("         (required) name = ");
                                    name = Console.ReadLine();
                                }
                                Console.WriteLine();
                                Console.WriteLine("         Please select the Admin for the group Created");
                                foreach (var user in users)
                                {
                                    Console.WriteLine("         " + user.ID + " " + user.Username);
                                }
                                int id;
                                string idString = Console.ReadLine();
                                bool validId = false;

                                while (!int.TryParse(idString, out id))
                                {
                                    Console.WriteLine("         Bad input for ID (choose a valid ID from above!");
                                    Console.Write("         id = ");
                                    idString = Console.ReadLine();
                                }
                                foreach (var user in users)
                                {
                                    if (user.ID == id)
                                    {
                                        validId = true;
                                        break;
                                    }
                                }
                                if (!validId)
                                {
                                    Console.WriteLine("         There is not such ID!");
                                }
                                else
                                {
                                    Group group = new Group();
                                    group.Name = name;
                                    group.AdminID = id;
                                    groupRepository.Insert(group);
                                    uow.SaveChanges();
                                }
                                break;
                            case ConsoleKey.D2:
                                groups = groupRepository.GetAll().ToList();
                                foreach (var g in groups)
                                {
                                    Console.WriteLine("      ID: " + g.ID + "   name:" + g.Name);
                                }
                                Console.WriteLine("      Please choose an ID:");
                                idString = Console.ReadLine();
                                while (!int.TryParse(idString, out id))
                                {
                                    Console.WriteLine("      Incorrect input. Please try again : ");
                                    idString = Console.ReadLine();
                                }
                                bool isIdValid = false;
                                foreach (var g in groups)
                                {
                                    if (g.ID == id)
                                    {
                                        isIdValid = true;
                                        break;
                                    }
                                }
                                if (isIdValid)
                                {
                                    Group groupToBeDeleted = groupRepository.GetByID(id);
                                    groupRepository.Delete(groupToBeDeleted);
                                    uow.SaveChanges();
                                    Console.WriteLine("The selected group was deleted successfully!");
                                }
                                else
                                {
                                    Console.WriteLine("There is no Group with such ID!");
                                }
                                break;
                            case ConsoleKey.D3:
                                groups = groupRepository.GetAll().ToList();
                                foreach (var g in groups)
                                {
                                    Console.WriteLine("      ID: " + g.ID + "   name:" + g.Name + "    admin : " + g.AdminID);
                                }
                                Console.WriteLine("      Please choose an ID:");
                                idString = Console.ReadLine();
                                while (!int.TryParse(idString, out id))
                                {
                                    Console.WriteLine("      Incorrect input. Please try again : ");
                                    idString = Console.ReadLine();
                                }
                                isIdValid = false;
                                foreach (var g in groups)
                                {
                                    if (g.ID == id)
                                    {
                                        isIdValid = true;
                                        break;
                                    }
                                }
                                if (isIdValid)
                                {
                                    Group groupToBeEdited = groupRepository.GetByID(id);

                                    Console.WriteLine("      Please introduce the new name of the group:");
                                    var newName = Console.ReadLine();

                                    Console.WriteLine("      Please introduce the new admin of the group (choose from below) :");
                                    users = userRepository.GetAll().ToList();
                                    foreach (var user in users)
                                    {
                                        Console.WriteLine("      " + user.ID + " " + user.Username);
                                    }
                                    idString = Console.ReadLine();
                                    validId = false;

                                    while (!int.TryParse(idString, out id))
                                    {
                                        Console.WriteLine("         Bad input for ID (choose a valid ID from above!");
                                        Console.Write("         id = ");
                                        idString = Console.ReadLine();
                                    }
                                    
                                    foreach (var user in users)
                                    {
                                        if (user.ID == id)
                                        {
                                            validId = true;
                                            break;
                                        }
                                    }
                                    if (!validId)
                                    {
                                        Console.WriteLine("         There is not such ID!");
                                    }
                                    else
                                    {
                                        var newAdmin = id;
                                        groupToBeEdited.AdminID = id;
                                        groupToBeEdited.Name = newName;
                                        groupRepository.Edit(groupToBeEdited);
                                        uow.SaveChanges();
                                        Console.WriteLine("The selected group was edited successfully!");
                                    }

                                }
                                else
                                {
                                    Console.WriteLine("There is no group with such ID!");
                                }
                                break;
                            case ConsoleKey.D4:
                                groups = groupRepository.GetAll().ToList();
                                foreach (var g in groups)
                                {
                                    Console.WriteLine("      ID -> " + g.ID);
                                    Console.WriteLine("         Name -> " + g.Name);
                                    Console.WriteLine("         Admin ID -> " + g.AdminID + "\n         Admin username -> " + userRepository.GetByID(g.AdminID).Username);
                                    
                                }
                                break;
                            case ConsoleKey.D5:
                                groups = groupRepository.GetAll().ToList();
                                if(groups.Count == 0)
                                {
                                    Console.WriteLine("      There are no groups in the database!");
                                    break;
                                }
                                Console.WriteLine("      Please choose the ID of the group you want to add users to!");
                                foreach(var g in groups)
                                {
                                    Console.WriteLine("          " + g.ID + " : " + g.Name);
                                }
                                idString = Console.ReadLine();
                                int groupID;
                                while(!int.TryParse(idString, out groupID))
                                {
                                    Console.WriteLine("      Wrong ID input. Please introduce the ID again!");
                                    Console.Write("         id = ");
                                    idString = Console.ReadLine();
                                }
                                bool validGroupID = false;
                                foreach(var g in groups)
                                {
                                    if (g.ID == groupID)
                                    {
                                        validGroupID = true;
                                        break;
                                    }
                                }
                                if (!validGroupID)
                                {
                                    Console.WriteLine("      There is not such a group ID!");
                                    break;
                                }
                                Console.WriteLine("      Please choose the ID of the user you want to add to the group!");
                                users = userRepository.GetAll().ToList();
                                foreach (var u in users)
                                {
                                    Console.WriteLine("          " + u.ID + " : " + u.Username);
                                }
                                idString = Console.ReadLine();
                                int userID;
                                while (!int.TryParse(idString, out userID))
                                {
                                    Console.WriteLine("      Wrong ID input. Please introduce the ID again!");
                                    Console.Write("         id = ");
                                    idString = Console.ReadLine();
                                }
                                bool validUserID = false;
                                foreach (var u in users)
                                {
                                    if (u.ID == userID)
                                    {
                                        validUserID = true;
                                        break;
                                    }
                                }
                                if (!validUserID)
                                {
                                    Console.WriteLine("      There is not such a user ID!");
                                    break;
                                }
                                User newUser = userRepository.GetByID(userID);
                                Group newGroup = groupRepository.GetByID(groupID);

                                var pk1 = new object[] { (object)userID, (object)groupID };
                                UserGroup ug1 = usergroupRepository.GetByID(pk1);
                                if (ug1 == null)
                                {
                                    UserGroup usergroup = new UserGroup { Group = newGroup, User = newUser };
                                    usergroupRepository.Insert(usergroup);
                                    uow.SaveChanges();
                                }
                                else
                                {
                                    Console.WriteLine("      The selected user is already a member of the group!");
                                }
                                uow.SaveChanges();

                                break;

                            case ConsoleKey.D6:
                                Console.WriteLine("      Please choose the ID of the group you want to see the users!");
                                groups = groupRepository.GetAll().ToList();
                                foreach (var g in groups)
                                {
                                    Console.WriteLine("          " + g.ID + " : " + g.Name);
                                }
                                idString = Console.ReadLine();
                                while (!int.TryParse(idString, out groupID))
                                {
                                    Console.WriteLine("      Wrong ID input. Please introduce the ID again!");
                                    Console.Write("         id = ");
                                    idString = Console.ReadLine();
                                }
                                validGroupID = false;

                                foreach (var g in groups)
                                {
                                    if (g.ID == groupID)
                                    {
                                        validGroupID = true;
                                        break;
                                    }
                                }
                                if (!validGroupID)
                                {
                                    Console.WriteLine("      There is not such a group ID!");
                                    break;
                                }
                                List<UserGroup> ugs = usergroupRepository.GetAll().ToList();
                                Group group1 = groupRepository.GetByID(groupID);

                                if (group1.UserGroups == null)
                                {
                                    Console.WriteLine("      There aren't any users in this group!");
                                    break;
                                }
                                foreach(UserGroup ug in group1.UserGroups)
                                {
                                    Console.WriteLine("      " + ug.User.Username);
                                }
                                
                                break;
                        }
                    } while (input.Key != ConsoleKey.D0);
                }
            }
        }
        public static void FriendshipManagement()
        {
            using (var db = new HangoutsContext(new Microsoft.EntityFrameworkCore.DbContextOptions<HangoutsContext>()))
            {
                DbInitializer.Initialize(db);

                using (var uow = new UnitOfWork())
                {
                    var friendshipRepository = uow.GetRepository<Friendship>();
                    var userRepository = uow.GetRepository<User>();
                    ConsoleKeyInfo input;
                    List<Friendship> friendships = friendshipRepository.GetAll().ToList();
                    List<User> users = userRepository.GetAll().ToList() ;
                    do
                    {
                        Console.WriteLine("     Menu friendship management:");
                        Console.WriteLine("     0 - Exit");
                        Console.WriteLine("     1 - Create a new friendship");
                        Console.WriteLine("     2 - Delete an existent friendship");
                        Console.WriteLine("     3 - View all friendships");
                        Console.WriteLine("     4 - View all friends for a specific user");

                        input = Console.ReadKey();
                        Console.WriteLine();

                        switch (input.Key)
                        {
                            case ConsoleKey.D1:
                                if(users.Count < 2)
                                {
                                    Console.WriteLine("          There must be minimum two users in the database to create a friendship relation");
                                    break;
                                }
                                Console.WriteLine("          Choose the IDs of two users to create a friendship relation between them:");
                                foreach(var user in users)
                                {
                                    Console.WriteLine("          ID: " + user.ID + " Username : " + user.Username);
                                }
                                int id1, id2;
                                string idString1, idString2;
                                bool validId1 = false, validId2 = false;
                                Console.WriteLine("         Please select ID1 : ");
                                idString1 = Console.ReadLine();
                                while (!int.TryParse(idString1, out id1))
                                {
                                    Console.WriteLine("         Bad input for ID1 (choose a valid ID from above!");
                                    Console.Write("         id1 = ");
                                    idString1 = Console.ReadLine();
                                }
                                Console.WriteLine("         Please select ID2 : ");
                                idString2 = Console.ReadLine();
                                while (!int.TryParse(idString2, out id2))
                                {
                                    Console.WriteLine("         Bad input for ID2 (choose a valid ID from above!");
                                    Console.Write("         id2 = ");
                                    idString2 = Console.ReadLine();
                                }
                                foreach (var user in users)
                                {
                                    if (user.ID == id1)
                                    {
                                        validId1 = true;
                                    }
                                    if(user.ID == id2)
                                    {
                                        validId2 = true;
                                    }
                                }
                                if (!validId1 || !validId2)
                                {
                                    if(!validId1)
                                        Console.WriteLine("         There is not user with ID1!");
                                    else 
                                        Console.WriteLine("         There is not user with ID2!");
                                    break;
                                }
                                else
                                {
                                    if (id1 == id2)
                                    {
                                        Console.WriteLine("It is not possible to create a friendship relation between two identical IDs");
                                        break;
                                    }
                                    var pk1 = new object[] { (object)id1, (object)id2 };
                                    var pk2 = new object[] { (object)id2, (object)id1 };
                                    Friendship f1 = friendshipRepository.GetByID(pk1);
                                    Friendship f2 = friendshipRepository.GetByID(pk2);
                                    if(f2 == null && f1 == null)
                                    {
                                        Friendship friendship = new Friendship { UserID1 = id1, UserID2 = id2 };
                                        uow.SaveChanges();
                                        friendshipRepository.Insert(friendship);
                                        Console.WriteLine("A friendship relation between ID " + id1 +" and ID " + id2 +" was successfully created!");
                                        uow.SaveChanges();
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("There is already a friendship relation between ID " + id1 + " and ID " + id2 + "!");
                                    }
                                    break;
                                }
                            case ConsoleKey.D2:
                                friendships = friendshipRepository.GetAll().ToList();
                                foreach (var friendship in friendships)
                                {
                                    Console.WriteLine("         ID1: " + friendship.UserID1 + " username1 :" + friendship.User1.Username +
                                        " is friend with " + " ID2 : " + friendship.UserID2 + " username2 :" + friendship.User2.Username);
                                }
                                Console.WriteLine("          Choose the IDs of the two users you want to delete the relationship:");

                                validId1 = false;
                                validId2 = false;
                                Console.WriteLine("         Please select ID1 : ");
                                idString1 = Console.ReadLine();
                                while (!int.TryParse(idString1, out id1))
                                {
                                    Console.WriteLine("         Bad input for ID1 (choose a valid ID from above!");
                                    Console.Write("         id1 = ");
                                    idString1 = Console.ReadLine();
                                }
                                Console.WriteLine("         Please select ID2 : ");
                                idString2 = Console.ReadLine();
                                while (!int.TryParse(idString2, out id2))
                                {
                                    Console.WriteLine("         Bad input for ID2 (choose a valid ID from above!");
                                    Console.Write("         id2 = ");
                                    idString2 = Console.ReadLine();
                                }

                                var prk1 = new object[] { (object)id1, (object)id2 };
                                var prk2 = new object[] { (object)id2, (object)id1 };
                                Friendship fr1 = friendshipRepository.GetByID(prk1);
                                Friendship fr2 = friendshipRepository.GetByID(prk2);
                                if (fr2 == null && fr1 == null)
                                {
                                    Console.WriteLine("There is no friendship relation between ID " + id1 + " and ID " + id2 + "!");
                                } else
                                {
                                    if(fr1 == null) { 
                                        friendshipRepository.Delete(fr2);
                                        uow.SaveChanges();
                                    }
                                    else
                                    {
                                        friendshipRepository.Delete(fr1);
                                        uow.SaveChanges();
                                    }
                                    Console.WriteLine("The friendship relation between ID " + id1 + " and ID " + id2 + " was successfully deleted!");
                                    uow.SaveChanges();
                                }

                                    break;
                            case ConsoleKey.D3:
                                friendships = friendshipRepository.GetAll().ToList();
                                foreach (var friendship in friendships)
                                {
                                    Console.WriteLine("         ID1: " + friendship.UserID1 + " username1 :" + friendship.User1.Username + 
                                        " is friend with " + " ID2 : " + friendship.UserID2 + " username2 :" + friendship.User2.Username);
                                }
                                break;
                            case ConsoleKey.D4:
                                users = userRepository.GetAll().ToList();
                                foreach(var user in users)
                                {
                                    Console.WriteLine("         " + user.ID + " " + user.Username);
                                }
                                Console.WriteLine("         Please choose an ID from above");
                                string idString = Console.ReadLine();
                                bool validId = false;
                                int id;
                                while (!int.TryParse(idString, out id))
                                {
                                    Console.WriteLine("         Bad input for ID (choose a valid ID from above!");
                                    Console.Write("         id = ");
                                    idString = Console.ReadLine();
                                }

                                foreach (var user in users)
                                {
                                    if (user.ID == id)
                                    {
                                        validId = true;
                                        break;
                                    }
                                }
                                if (!validId)
                                {
                                    Console.WriteLine("         There is not such ID!");
                                }
                                else
                                {
                                    var user = userRepository.GetByID(id);
                                    Console.WriteLine("         " + user.Username + " has the following friends: ");
                                    foreach (var friend in user.Friends)
                                    {
                                        Console.WriteLine("         " + friend.Username);
                                    }
                                }
                                break;
                        }
                    } while (input.Key != ConsoleKey.D0);
                }
            }
        }
    }
}

