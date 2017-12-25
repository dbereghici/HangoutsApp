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
