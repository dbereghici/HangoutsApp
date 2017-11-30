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

        public void AddUser(User user)
        {
            using (var uow = new UnitOfWork())
            {
                var userRepository = uow.GetRepository<User>();
                userRepository.Insert(user);
                uow.SaveChanges();
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

        public void UpdateUser(int id, User user)
        {
            using (var uow = new UnitOfWork())
            {
                var userRepository = uow.GetRepository<User>();
                User userToUpdate = userRepository.GetByID(id);
                userToUpdate.Address = user.Address;
                userToUpdate.BirthDate = user.BirthDate;
                userToUpdate.Email = user.Email;
                userToUpdate.LastName = user.LastName;
                userToUpdate.FirstName = user.FirstName;
                userToUpdate.Username = user.Username;
                userToUpdate.Password = user.Password;

                userRepository.Edit(userToUpdate);
                uow.SaveChanges();
            }
        }

        public void DeleteUser(int id)
        {
            using (var uow = new UnitOfWork())
            {
                var userRepository = uow.GetRepository<User>();
                User user = userRepository.GetByID(id);
                userRepository.Delete(user);
                uow.SaveChanges();
            }
        }
    }
}
