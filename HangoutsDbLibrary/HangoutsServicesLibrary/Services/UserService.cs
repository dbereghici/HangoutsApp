using HangoutsDbLibrary.Model;
using HangoutsDbLibrary.Repository;
using Microsoft.EntityFrameworkCore;
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

        public string AddUser(User user)
        {
            using (var uow = new UnitOfWork())
            {
                var userRepository = uow.GetRepository<User>();
                var addressRepository = uow.GetRepository<Address>();
                // Check if there is already a user with username / email data
                var existUser = userRepository.GetAll().Where(u => u.Email == user.Email).FirstOrDefault();
                if (existUser != null)
                    return "This email is already used";
                existUser = userRepository.GetAll().Where(u => u.Username == user.Username).FirstOrDefault();
                if (existUser == null)
                {
                    // Check if the user's location is already in DB
                    Address existAddress = addressRepository.GetAll().Where(u => u.Latitude == user.Address.Latitude || u.Longitude == user.Address.Longitude).FirstOrDefault();
                    if (existAddress != null)
                        user.Address = existAddress;
                    userRepository.Insert(user);
                    uow.SaveChanges();
                    return "Ok"; ;
                }
                else
                {
                    return "This username is already used";
                }

            }
        }

        public string UpdateUser(int id, User user)
        {
            using (var uow = new UnitOfWork())
            {
                var userRepository = uow.GetRepository<User>();
                User userToUpdate = userRepository.GetByID(id);

                if(userToUpdate == null)
                {
                    return "Invalid ID";
                }

                var addressRepository = uow.GetRepository<Address>();
                // Check if there is already a user with email data
                var existUser = userRepository.GetAll().Where(u => u.Email == user.Email).FirstOrDefault();
                if (existUser != null && existUser.ID != userToUpdate.ID)
                    return "This email is already used";
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
                    return "Ok";
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

        public string DeleteUser(int id)
        {
            using (var uow = new UnitOfWork())
            {
                var userRepository = uow.GetRepository<User>();
                User user = userRepository.GetByID(id);
                if(user == null)
                {
                    return "invalid ID";
                }
                userRepository.Delete(user);
                uow.SaveChanges();
                return "Ok";
            }
        }
    }
}
