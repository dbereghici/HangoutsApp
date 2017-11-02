using HangoutsDbLibrary.Data;
using HangoutsDbLibrary.Model;
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

                // db.Users.Add(new User { Username = "Mihai" });


                // //db.Groups.Add(new Group { GroupNr = "asd" });
                //// db.SaveChanges();

                // db.Activities.Add(new Activity { Description = "descrier12" });
                // db.SaveChanges();
                // var activity = db.Activities.FirstOrDefault();

                // var x = db.Groups.Include(gr => gr.UserGroups).ToList();

                // var z = db.Activities.ToList();
                // var y = db.Groups.ToList();

                // activity.Group = db.Groups.FirstOrDefault(g => g.GroupNr == "asd");

                // var group = db.Groups.FirstOrDefault();
                // //db.Remove(activity);
                // var count = db.SaveChanges();
                // Console.WriteLine("{0} records saved to database", count);

                // Console.WriteLine();
                // Console.WriteLine("All Users in database:");
                // foreach (var user in db.Users)
                // {
                //     Console.WriteLine(" - {0}", user.Username);
                // }

                //var userGroup = new UserGroup { GroupId = db.Groups.Single(g => g.GroupNr == "Group1").GroupNr, UserId = db.Users.Single(u => u.Username == "Mihai").Id };

                //db.UserGroups.Add(userGroup);
                //db.SaveChanges();

                
            }
        }
    }
}
