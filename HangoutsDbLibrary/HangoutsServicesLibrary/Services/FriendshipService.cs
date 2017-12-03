using HangoutsDbLibrary.Model;
using HangoutsDbLibrary.Repository;
using HangoutsWebApi.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HangoutsBusinessLibrary.Services
{
    public class FriendshipService
    {
        public List<Friendship> GetAllFriendships()
        {
            using(var uow = new UnitOfWork())
            {
                var friendshipRepository = uow.GetRepository<Friendship>();
                List<Friendship> friendships = friendshipRepository.GetAll().Include(f => f.User1).Include(f => f.User2).ToList();
                return friendships;
            }
        }

        public Friendship GetByID(int id1, int id2)
        {
            using (var uow = new UnitOfWork())
            {
                var friendshipRepository = uow.GetRepository<Friendship>();
                var pk = new object[] { (object)id1, (object)id2 };
                //Friendship friendship = friendshipRepository.GetByID(pk);
                Friendship friendship = friendshipRepository
                    .GetAll()
                    .Include(f => f.User1)
                    .Include(f => f.User2)
                    .Where(f => f.UserID1 == id1 && f.UserID2 == id2)
                    .FirstOrDefault();
                if (friendship == null)
                {
                    friendship = friendshipRepository
                    .GetAll()
                    .Include(f => f.User1)
                    .Include(f => f.User2)
                    .Where(f => f.UserID1 == id2 && f.UserID2 == id1)
                    .FirstOrDefault();
                }
                return friendship;
            }
        }

        public string AddFriendship(Friendship friendship)
        {
            UserService userService = new UserService();
            // Check for users
            int id1 = friendship.UserID1;
            int id2 = friendship.UserID2;
            User user1 = userService.GetByID(id1);
            User user2 = userService.GetByID(id2);
            if (user1 == null)
                return "There is no user with ID :" + id1;
            if (user2 == null)
                return "There is no user with ID :" + id2;
            using (var uow = new UnitOfWork())
            {
                var friendshipRepository = uow.GetRepository<Friendship>();
                // Check for existent friendship
                var pk = new object[] { (object)id1, (object)id2 };
                var existFriendship = friendshipRepository.GetByID(pk);
                if (existFriendship == null)
                {
                    pk = new object[] { (object)id2, (object)id1 };
                    existFriendship = friendshipRepository.GetByID(pk);
                }
                if (existFriendship != null)
                    return "There already is a friendship relation between user" + id1 + " and user" + id2 + " Status = " + existFriendship.Status;
                // Add friendship 
                friendship.User1 = null;
                friendship.User2 = null;
                friendship.UserID1 = id1;
                friendship.UserID2 = id2;
                friendship.Chat = new Chat();
                friendshipRepository.Insert(friendship);
                uow.SaveChanges();
                return "Ok";
            }
        }

        public string UpdateFriendship(Friendship friendship)
        {
            UserService userService = new UserService();
            // Check for users
            int id1 = friendship.UserID1;
            int id2 = friendship.UserID2;
            User user1 = userService.GetByID(id1);
            User user2 = userService.GetByID(id2);
            if (user1 == null)
                return "There is no user with ID :" + id1;
            if (user2 == null)
                return "There is no user with ID :" + id2;
            using (var uow = new UnitOfWork())
            {
                var friendshipRepository = uow.GetRepository<Friendship>();
                // Check for existent friendship
                var pk = new object[] { (object)id1, (object)id2 };
                var friendshipToUpdate = friendshipRepository.GetByID(pk);
                if (friendshipToUpdate == null)
                {
                    pk = new object[] { (object)id2, (object)id1 };
                    friendshipToUpdate = friendshipRepository.GetByID(pk);
                }
                if (friendshipToUpdate == null)
                    return "There is no friendship relation between user" + id1 + " and user" + id2;
                // Update Friendship 
                friendshipToUpdate.Status = friendship.Status;
                friendshipRepository.Edit(friendshipToUpdate);
                uow.SaveChanges();
                return "Ok";
            }
        }

        public string DeleteFriendship(int id1, int id2)
        {
            UserService userService = new UserService();
            // Check for users
            User user1 = userService.GetByID(id1);
            User user2 = userService.GetByID(id2);
            if (user1 == null)
                return "There is no user with ID :" + id1;
            if (user2 == null)
                return "There is no user with ID :" + id2;
            using (var uow = new UnitOfWork())
            {
                var friendshipRepository = uow.GetRepository<Friendship>();
                // Check for existent friendship
                var pk = new object[] { (object)id1, (object)id2 };
                var friendshipToDelete = friendshipRepository.GetByID(pk);
                if (friendshipToDelete == null)
                {
                    pk = new object[] { (object)id2, (object)id1 };
                    friendshipToDelete = friendshipRepository.GetByID(pk);
                }
                if (friendshipToDelete == null)
                    return "There is no friendship relation between user" + id1 + " and user" + id2;
                // Delete Friendship 
                friendshipRepository.Delete(friendshipToDelete);
                uow.SaveChanges();
                return "Ok";
            }
        }


    }
}
