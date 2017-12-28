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
            using (var uow = new UnitOfWork())
            {
                var friendshipRepository = uow.GetRepository<Friendship>();
                List<Friendship> friendships = friendshipRepository.GetAll().Include(f => f.User1).Include(f => f.User2).ToList();
                return friendships;
            }
        }

        public List<User> GetAllFriendRequestsMade(int id)
        {
            using (var uow = new UnitOfWork())
            {
                var userRepository = uow.GetRepository<User>();
                User user = userRepository
                    .GetAll()
                    .Include(u => u.FriendRequestsAccepted)
                    .ThenInclude(f => f.User2)
                    .ThenInclude(u => u.Address)
                    .Include(u => u.FriendRequestsMade)
                    .ThenInclude(f => f.User1)
                    .ThenInclude(u => u.Address)
                    .Where(u => u.ID == id)
                    .FirstOrDefault();
                if (user == null)
                    throw new Exception("Invalid id");
                List<User> friendRequestMade = new List<User>();
                foreach (var friendship in user.FriendRequestsMade)
                {
                    if (friendship.Status.Equals("pending"))
                        friendRequestMade.Add(friendship.User1);
                }
                return friendRequestMade;
            }
        }

        public List<User> GetAllFriendRequestReceived(int id)
        {
            using (var uow = new UnitOfWork())
            {
                var userRepository = uow.GetRepository<User>();
                User user = userRepository
                     .GetAll()
                     .Include(u => u.FriendRequestsAccepted)
                     .ThenInclude(f => f.User2)
                     .ThenInclude(u => u.Address)
                     .Include(u => u.FriendRequestsMade)
                     .ThenInclude(f => f.User1)
                     .ThenInclude(u => u.Address)
                     .Where(u => u.ID == id)
                     .FirstOrDefault();
                if (user == null)
                    throw new Exception("Invalid id");
                List<User> friendRequestMade = new List<User>();
                foreach (var friendship in user.FriendRequestsAccepted)
                {
                    if (friendship.Status.Equals("pending"))
                        friendRequestMade.Add(friendship.User2);
                }
                return friendRequestMade;
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

        public Friendship AddFriendship(Friendship friendship)
        {
            UserService userService = new UserService();
            // Check for users
            int id1 = friendship.UserID1;
            int id2 = friendship.UserID2;
            User user1 = userService.GetByID(id1);
            User user2 = userService.GetByID(id2);
            if (user1 == null)
                throw new Exception("There is no user with ID :" + id1);
            if (user2 == null)
                throw new Exception("There is no user with ID :" + id2);
            using (var uow = new UnitOfWork())
            {
                var friendshipRepository = uow.GetRepository<Friendship>();
                var userChatRepository = uow.GetRepository<UserChat>();
                // Check for existent friendship
                var pk = new object[] { (object)id1, (object)id2 };
                var existFriendship = friendshipRepository.GetByID(pk);
                if (existFriendship == null)
                {
                    pk = new object[] { (object)id2, (object)id1 };
                    existFriendship = friendshipRepository.GetByID(pk);
                }
                if (existFriendship != null)
                    throw new Exception("There already is a friendship relation between user" + id1 + " and user" + id2 + " Status = " + existFriendship.Status);
                // Add friendship 
                friendship.User1 = null;
                friendship.User2 = null;
                friendship.UserID1 = id1;
                friendship.UserID2 = id2;
                friendship.Chat = new Chat();
                friendshipRepository.Insert(friendship);
                uow.SaveChanges();
                // Add users to chat
                pk = new object[] { (object)id1, (object)id2 };
                friendship = friendshipRepository.GetByID(pk);
                if (friendship == null)
                {
                    pk = new object[] { (object)id2, (object)id1 };
                    friendship = friendshipRepository.GetByID(pk);
                }
                UserChat userChat = new UserChat { ChatID = friendship.ChatID, UserID = id1 };
                userChatRepository.Insert(userChat);
                userChat = new UserChat { ChatID = friendship.ChatID, UserID = id2 };
                userChatRepository.Insert(userChat);
                uow.SaveChanges();
                return friendship;
            }
        }

        public Friendship UpdateFriendship(Friendship friendship)
        {
            UserService userService = new UserService();
            // Check for users
            int id1 = friendship.UserID1;
            int id2 = friendship.UserID2;
            User user1 = userService.GetByID(id1);
            User user2 = userService.GetByID(id2);
            if (user1 == null)
                throw new Exception("There is no user with ID :" + id1);
            if (user2 == null)
                throw new Exception("There is no user with ID :" + id2);
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
                    throw new Exception("There is no friendship relation between user" + id1 + " and user" + id2);
                // Update Friendship 
                friendshipToUpdate.Status = friendship.Status;
                friendshipRepository.Edit(friendshipToUpdate);
                uow.SaveChanges();
                return friendship;
            }
        }

        public void DeleteFriendship(int id1, int id2)
        {
            UserService userService = new UserService();
            // Check for users
            User user1 = userService.GetByID(id1);
            User user2 = userService.GetByID(id2);
            if (user1 == null)
                throw new Exception("There is no user with ID :" + id1);
            if (user2 == null)
                throw new Exception("There is no user with ID :" + id2);
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
                    throw new Exception("There is no friendship relation between user" + id1 + " and user" + id2);
                // Delete Friendship 
                friendshipRepository.Delete(friendshipToDelete);
                uow.SaveChanges();
            }
        }


    }
}
