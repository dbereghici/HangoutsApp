﻿using HangoutsBusinessLibrary.Services;
using HangoutsDbLibrary.Model;
using HangoutsDbLibrary.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HangoutsWebApi.Services
{
    public class UserService
    {
        public User GetByID(int id)
        {
            using (var uow = new UnitOfWork())
            {
                var userRepository = uow.GetRepository<User>();
                var addressRepository = uow.GetRepository<Address>();

                User user = userRepository.GetByID(id);
                if(user != null) { 
                    user.Address = addressRepository.GetByID(user.AddressID);
                }
                return user;
            }
        }

        public List<User> GetAllUsers()
        {
            using (var uow = new UnitOfWork())
            {
                var userRepository = uow.GetRepository<User>();
                List<User> users = userRepository.GetAll().Include(u => u.Address).ToList();
                return users;
            }
        }

        public List<User> GetAllUsersExceptId(int id)
        {
            using (var uow = new UnitOfWork())
            {
                var userRepository = uow.GetRepository<User>();
                List<User> users = userRepository.GetAll().Where(u => u.ID != id).Include(u => u.Address).ToList();
                return users;
            }
        }

        public User AddUser(User user)
        {
            using (var uow = new UnitOfWork())
            {
                var userRepository = uow.GetRepository<User>();
                var addressRepository = uow.GetRepository<Address>();
                // Check if there is already a user with username / email data
                var existUser = userRepository.GetAll().Where(u => u.Email == user.Email).FirstOrDefault();
                if (existUser != null)
                    throw new Exception("This email is already used");
                existUser = userRepository.GetAll().Where(u => u.Username == user.Username).FirstOrDefault();
                if (existUser == null)
                {
                    // Check if the user's location is already in DB
                    Address existAddress = addressRepository.GetAll().Where(u => u.Latitude == user.Address.Latitude || u.Longitude == user.Address.Longitude).FirstOrDefault();
                    if (existAddress != null)
                        user.Address = existAddress;
                    userRepository.Insert(user);
                    uow.SaveChanges();
                    return user;
                }
                else
                {
                    throw new Exception("This username is already used");
                }

            }
        }

        public User UpdateUser(int id, User user)
        {
            using (var uow = new UnitOfWork())
            {
                var userRepository = uow.GetRepository<User>();
                User userToUpdate = userRepository.GetByID(id);

                if(userToUpdate == null)
                {
                    throw new Exception("Invalid ID");
                }

                var addressRepository = uow.GetRepository<Address>();
                // Check if there is already a user with email data
                var existUser = userRepository.GetAll().Where(u => u.Email == user.Email).FirstOrDefault();
                if (existUser != null && existUser.ID != userToUpdate.ID)
                    throw new Exception("This email is already used");
                else
                {
                    // Check if the user's location is already in DB
                    Address existAddress = addressRepository.GetAll().Where(u => u.Latitude == user.Address.Latitude || u.Longitude == user.Address.Longitude).FirstOrDefault();
                    if (existAddress != null)
                        user.Address = existAddress;

                    userToUpdate.Address = user.Address;
                    userToUpdate.BirthDate = user.BirthDate;
                    userToUpdate.Email = user.Email;
                    userToUpdate.LastName = user.LastName;
                    userToUpdate.FirstName = user.FirstName;
                    userToUpdate.Username = user.Username;
                    userToUpdate.Password = user.Password;

                    userRepository.Edit(userToUpdate);
                    uow.SaveChanges();
                    return userToUpdate;
                }
            }
        }

        public List<User> GetAllFriends(int id)
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
                List<User> friends = user.Friends;
                if (user != null)
                    return user.Friends;
                else
                    return null;
            }
        }

        public List<User> SearchForUsersOfGroup(int userid, int groupid, string searchString)
        {
            using (var uow = new UnitOfWork())
            {
                var userRepository = uow.GetRepository<User>();
                var friendshipRepository = uow.GetRepository<Friendship>();
                var groupRepository = uow.GetRepository<Group>();
                var userGroupRepository = uow.GetRepository<UserGroup>();
                User user = userRepository.GetByID(userid);
                if (user == null)
                {
                    throw new Exception("Invalid id");
                }
                List<User> users = new List<User>();
                List<User> intermedUsers = new List<User>();
                if (searchString == null || searchString.Equals(""))
                    users = userRepository.GetAll().Where(u => u.ID != userid).ToList();
                else
                {
                    char[] delimiterChar = { ' ' };
                    string[] words = searchString.Split(delimiterChar);

                    foreach (var word in words)
                    {
                        intermedUsers = userRepository
                            .GetAll()
                            .Where(u => ((u.FirstName.Contains(word) || u.LastName.Contains(word) || u.Username.Contains(word))
                            && u.ID != userid))
                        .ToList();
                        users = users.Concat(intermedUsers).ToList();
                    }
                }
                Group group = groupRepository.GetByID(groupid);
                if (group == null)
                    throw new Exception("invalid group id");
                List<User> result = new List<User>();

                foreach (var u in users)
                {
                    UserGroup usergroup = userGroupRepository.GetAll().Where(ug => ug.GroupID == groupid && ug.UserID == u.ID && ug.UserID != user.ID).FirstOrDefault();
                    if (usergroup != null && (usergroup.Status.Equals("member") || usergroup.Status.Equals("admin")))
                        result.Add(userRepository.GetByID(usergroup.UserID));
                }
                return result;
            }
        }

        public List<User> SearchForUsersOfPlan(int userid, int planId, string searchString)
        {
            using (var uow = new UnitOfWork())
            {
                var userRepository = uow.GetRepository<User>();
                var friendshipRepository = uow.GetRepository<Friendship>();
                var planRepository = uow.GetRepository<Plan>();
                var planUserRepository = uow.GetRepository<PlanUser>();
                User user = userRepository.GetByID(userid);
                if (user == null)
                {
                    throw new Exception("Invalid id");
                }
                List<User> users = new List<User>();
                List<User> intermedUsers = new List<User>();
                if (searchString == null || searchString.Equals(""))
                    users = userRepository.GetAll().Where(u => u.ID != userid).ToList();
                else
                {
                    char[] delimiterChar = { ' ' };
                    string[] words = searchString.Split(delimiterChar);

                    foreach (var word in words)
                    {
                        intermedUsers = userRepository
                            .GetAll()
                            .Where(u => ((u.FirstName.Contains(word) || u.LastName.Contains(word) || u.Username.Contains(word))
                            && u.ID != userid))
                        .ToList();
                        users = users.Concat(intermedUsers).ToList();
                    }
                }
                Plan plan = planRepository.GetByID(planId);
                if (plan == null)
                    throw new Exception("invalid plan id");
                List<User> result = new List<User>();

                foreach (var u in users)
                {
                    PlanUser planUser = planUserRepository.GetAll().Where(ug => ug.PlanID == planId && ug.UserID == u.ID && ug.UserID != user.ID).FirstOrDefault();
                    if (planUser != null)
                        result.Add(userRepository.GetByID(planUser.UserID));
                }
                return result;
            }
        }


        public List<User> SearchNewFriends(int id, string searchString)
        {
            using (var uow = new UnitOfWork())
            {
                var userRepository = uow.GetRepository<User>();
                var friendshipRepository = uow.GetRepository<Friendship>();
                User user = userRepository.GetByID(id);
                if (user == null)
                {
                    throw new Exception("Invalid id");
                }
                List<User> users = new List<User>();
                List<User> intermedUsers = new List<User>();
                if (searchString == null || searchString.Equals(""))
                    users = userRepository.GetAll().Where(u => u.ID != id).ToList();
                else
                {
                    char[] delimiterChar = { ' ' };
                    string[] words = searchString.Split(delimiterChar);

                    foreach (var word in words) { 
                        intermedUsers = userRepository
                            .GetAll()
                            .Where(u => ((u.FirstName.Contains(word) || u.LastName.Contains(word) || u.Username.Contains(word))
                            && u.ID != id))
                        .ToList();
                        users = users.Concat(intermedUsers).ToList();
                    }

                }

                List<User> result = new List<User>();
                foreach (var u in users)
                {
                    var pk = new object[] { (object)id, (object)u.ID };
                    if (friendshipRepository.GetByID(pk) == null)
                    {
                        pk = new object[] { (object)u.ID, (object)id };
                        if (friendshipRepository.GetByID(pk) == null)
                            result.Add(u);
                    }
                }

                return result;
            }
        }

        
        public List<User> SearchAllFriends(int id, string searchString)
        {
            using (var uow = new UnitOfWork())
            {
                var userRepository = uow.GetRepository<User>();
                var friendshipRepository = uow.GetRepository<Friendship>();
                User user = userRepository.GetByID(id);
                if (user == null)
                {
                    throw new Exception("Invalid id");
                }
                List<User> users = new List<User>();
                List<User> intermedUsers = new List<User>();
                if (searchString == null || searchString.Equals(""))
                    users = userRepository.GetAll().Where(u => u.ID != id).ToList();
                else
                {
                    char[] delimiterChar = { ' ' };
                    string[] words = searchString.Split(delimiterChar);

                    foreach (var word in words)
                    {
                        intermedUsers = userRepository
                            .GetAll()
                            .Where(u => ((u.FirstName.Contains(word) || u.LastName.Contains(word) || u.Username.Contains(word))
                            && u.ID != id))
                        .ToList();
                        users = users.Concat(intermedUsers).ToList();
                    }

                }
                return users;
            }
        }

        public void DeleteUser(int id)
        {
            using (var uow = new UnitOfWork())
            {
                var userRepository = uow.GetRepository<User>();
                User user = userRepository.GetByID(id);
                if(user == null)
                {
                    throw new Exception("Invalid ID");
                }
                var userGroupRepository = uow.GetRepository<UserGroup>();
                var planUserRepository = uow.GetRepository<PlanUser>();
                var userChatRepository = uow.GetRepository<UserChat>();
                var friendshipRepository = uow.GetRepository<Friendship>();
                var userGroupReferences = userGroupRepository.GetAll().Where(ug => ug.UserID == id).ToList();
                foreach (var r in userGroupReferences)
                    userGroupRepository.Delete(r);
                var planUserReferences = planUserRepository.GetAll().Where(pu => pu.UserID == id).ToList();
                foreach (var r in planUserReferences)
                    planUserRepository.Delete(r);
                var userChatReferences = userChatRepository.GetAll().Where(uc => uc.UserID == id).ToList();
                foreach (var r in userChatReferences)
                    userChatRepository.Delete(r);
                var friendshipReferences = friendshipRepository.GetAll().Where(f => f.UserID1 == id).ToList();
                foreach (var r in friendshipReferences)
                    friendshipRepository.Delete(r);
                friendshipReferences = friendshipRepository.GetAll().Where(f => f.UserID2 == id).ToList();
                foreach (var r in friendshipReferences)
                    friendshipRepository.Delete(r);
                uow.SaveChanges();

                userRepository.Delete(user);
                uow.SaveChanges();
            }
        }
    }
}
