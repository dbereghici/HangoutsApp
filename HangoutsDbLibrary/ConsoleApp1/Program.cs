using HangoutsDbLibrary.Data;
using HangoutsDbLibrary.Model;
using HangoutsDbLibrary.Repository;
using Microsoft.EntityFrameworkCore;
using System;
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
                    //userRepository.Insert(new User { Username = "Bereghici Dumitru" });
                    User u1 = userRepository.GetByID(9);
                    u1.Username = "Vasiu Alin";
                    userRepository.Edit(u1);
                    uow.SaveChanges();

                    var users = userRepository.GetAll().ToList();
                    foreach (var user in users)
                    {
                        Console.WriteLine(user.Username);
                    }
                }
            }
        }
    }
}
