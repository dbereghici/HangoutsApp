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

        public User AddUser(User user)
        {
            using (var uow = new UnitOfWork())
            {
                var userRepository = uow.GetRepository<User>();
                var addressRepository = uow.GetRepository<Address>();
                // Check if there is already a user with username / email data
                var existUser = userRepository.GetAll().Where(u => u.Email == user.Email || u.Username == user.Username).FirstOrDefault();
                if (existUser == null)
                {
                    // Check if the user's location is already in DB
                    Address existAddress = addressRepository.GetAll().Where(u => u.Latitude == user.Address.Latitude || u.Longitude == user.Address.Longitude).FirstOrDefault();
                    if (existAddress != null)
                        user.Address = existAddress;
                    userRepository.Insert(user);
                    uow.SaveChanges();
                    return user; ;
                }
                else
                {
                    return null;
                }

            }
        }

        public List<User> GetAllUsersFromAGroup(int groupId)
        {
            using (var uow = new UnitOfWork())
            {
                var userGroupRepository = uow.GetRepository<UserGroup>();
                List<User> users = new List<User>();
                List<UserGroup> userGroups = userGroupRepository.GetAll().Include(ug => ug.User).ToList();

                foreach(var ug in userGroups)
                {
                    if(ug.GroupID == groupId)
                    {
                        users.Add(ug.User);
                    }
                }
                return users;
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
                    return null;
                }

                var addressRepository = uow.GetRepository<Address>();
                // Check if there is already a user with email data
                var existUser = userRepository.GetAll().Where(u => u.Email == user.Email).FirstOrDefault();
                if (existUser == null || existUser.Username == user.Username)
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
                else
                {
                    return null;
                }
            }
        }

        public User DeleteUser(int id)
        {
            using (var uow = new UnitOfWork())
            {
                var userRepository = uow.GetRepository<User>();
                User user = userRepository.GetByID(id);
                if(user == null)
                {
                    return null;
                }
                userRepository.Delete(user);
                uow.SaveChanges();
                return user;
            }
        }
    }
}
