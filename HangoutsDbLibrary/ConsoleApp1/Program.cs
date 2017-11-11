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
                    //userRepository.Insert(new User { Username = "Bereghici Dumitru" });
                    //User u1 = userRepository.GetByID(9);
                    //u1.Username = "Vasiu Alin";
                    //userRepository.Edit(u1);
                    /*
                    User u3 = userRepository.GetByID(3);
                    User u4 = userRepository.GetByID(4);
                    User u8 = userRepository.GetByID(8);
                    User u9 = userRepository.GetByID(9);

                    Friendship f1 = new Friendship { User1 = u3, User2 = u8 };
                    Friendship f2 = new Friendship { User1 = u3, User2 = u9 };
                    Friendship f3 = new Friendship { User1 = u8, User2 = u9 };
                    Friendship f4 = new Friendship { User1 = u4, User2 = u9 };

                   
                    friendshipRepository.Insert(f1);
                    friendshipRepository.Insert(f2);
                    friendshipRepository.Insert(f3);
                    friendshipRepository.Insert(f4);


                    uow.SaveChanges();
                    */
                    /*
                    User u3 = userRepository.GetByID(3);
                    User u4 = userRepository.GetByID(4);
                    User u8 = userRepository.GetByID(8);
                    User u9 = userRepository.GetByID(9);

                    List<User> users = new List<User>{ u3, u4, u8, u9 };

                    List<Friendship> friendships = friendshipRepository.GetAll().ToList();
                    
                    foreach(var user in users)
                    {
                        Console.WriteLine(user.Username + " : ");
                        Console.Write("         ");
                        foreach(var friend in user.Friends)
                        {
                            Console.Write(friend.Username + " ");
                        }
                        Console.WriteLine();
                    }
                    */

                    /*
                    User u3 = userRepository.GetByID(12);
                    User u4 = userRepository.GetByID(14);

                    Friendship f1 = new Friendship { User1 = u3, User2=u4 };
                    friendshipRepository.Insert(f1);
                    uow.SaveChanges();
                    */

                    ConsoleKeyInfo input;
                    do
                    {
                        Console.WriteLine("Menu:");
                        Console.WriteLine("0 - Exit");
                        Console.WriteLine("1 - User Management");
                        Console.WriteLine("2 - Group Management");
                        input = Console.ReadKey();
                        Console.WriteLine();
                        switch (input.Key)
                        {
                            case ConsoleKey.D1:
                                UserManagement();                               
                                break;
                            case ConsoleKey.D2:

                                break;

                        }

                    }
                    while (input.Key != ConsoleKey.D0);
                    /*
                    uow.SaveChanges();

                    var users = userRepository.GetAll().ToList();
                    foreach (var user in users)
                    {
                        Console.WriteLine(user.Username);
                    }
                    */
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
    }
}
