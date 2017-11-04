using HangoutsDbLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HangoutsDbLibrary.Data
{
    public static class DbInitializer
    {
        public static void Initialize(HangoutsContext context)
        {
            context.Database.EnsureCreated();

            //Look for any Users.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
            {
                new User{Username = "Mihai"},
                new User{Username = "Adrian"},
                new User{Username = "George"},
                new User{Username = "Tudor"},
            };

            foreach (User user in users)
                context.Users.Add(user);

            context.SaveChanges();

            var activities = new Activity[]
            {
                new Activity{Description = "Football"},
                new Activity{Description = "Dancing"}
            };

            foreach (Activity activity in activities)
                context.Activities.Add(activity);

            context.SaveChanges();

            var groups = new Group[]
            {
                new Group{GroupNr = "Group1"},
                new Group{GroupNr = "Group2"},
                new Group{GroupNr = "Group3"}
            };

            foreach (Group group in groups)
                context.Groups.Add(group);

            context.SaveChanges();

            //One to many seed
            var activity1 = context.Activities.Where(a => a.Description == "Dancing").FirstOrDefault();
            activity1.Group = context.Groups.Where(g => g.GroupNr == "Group1").FirstOrDefault();
            context.SaveChanges();

            //Many to many seed 1st way
            var user1 = context.Users.FirstOrDefault();
            var userGroup = new UserGroup { GroupId = context.Groups.FirstOrDefault().GroupNr, UserId = context.Users.FirstOrDefault().Id };
            user1.UserGroups.Add(userGroup);
            context.Groups.FirstOrDefault().UserGroups.Add(userGroup);
            context.SaveChanges();

            //Many to many seed 2nd way
            context.UserGroups.Add(new UserGroup { GroupId = context.Groups.SingleOrDefault(g => g.GroupNr == "Group2").GroupNr, UserId = context.Users.SingleOrDefault(u => u.Username == "Adrian").Id });
            context.SaveChanges();
        }
    }
}
