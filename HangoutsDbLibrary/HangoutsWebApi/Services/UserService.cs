using HangoutsDbLibrary.Model;
using HangoutsDbLibrary.Repository;
using HangoutsWebApi.DTOModels;
using HangoutsWebApi.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangoutsWebApi.Services
{
    public class UserService
    {
        public User getByID(int id)
        {
            using (var uow = new UnitOfWork())
            {
                var userRepository = uow.GetRepository<User>();
                var addressRepository = uow.GetRepository<Address>();

                User user = userRepository.GetByID(id);
                user.Address = addressRepository.GetByID(user.AddressID);

                return user;

                //Address address = addressRepository.GetByID(user.AddressID);
                //UserMapper userMapper = new UserMapper();
                //UserDTO userDTO = new UserDTO();
                //userDTO = userMapper.MapToDTO(user);
                //return userDTO;
            }
        }

        public List<User> getAllUsers()
        {
            using (var uow = new UnitOfWork())
            {
                var userRepository = uow.GetRepository<User>();

                List<User> users = userRepository.GetAll().ToList();
                return users;
                //List<UserDTO> usersDTO = new List<UserDTO>();
                //UserMapper userMapper = new UserMapper();
                //UserDTO userDTO = new UserDTO();
                //foreach(User user in users)
                //{
                //    userDTO = userMapper.MapToDTO(user);
                //    usersDTO.Add(userDTO);
                //}
                //return usersDTO;
            }
        }

        public List<User> getAllUsersFromAGroup(int groupId)
        {
            using (var uow = new UnitOfWork())
            {
                var userRepository = uow.GetRepository<User>();
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
    }
}
