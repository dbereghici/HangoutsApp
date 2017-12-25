using AutoMapper;
using HangoutsDbLibrary.Model;
using HangoutsWebApi.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangoutsWebApi.Mappings
{
    public class UserMapper
    {
        public UserDTO Map(User user)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>();
            //    .ForMember(dest => dest.Password, op => op.Ignore());
            });
            IMapper mapper = config.CreateMapper();
            UserDTO userDTO = mapper.Map<User, UserDTO>(user);
            return userDTO;
        }


        public List<UserDTO> Map (List<User> users)
        {
            List<UserDTO> usersDTO = new List<UserDTO>();
            foreach(var user in users)
            {
                usersDTO.Add(Map(user));
            }
            return usersDTO;
        }

        public User Map(UserDTO userDTO)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDTO, User>()
                .ForMember(dst => dst.Address, op => op.Ignore());
            });
            IMapper mapper = config.CreateMapper();
            User user = mapper.Map<UserDTO, User>(userDTO);

            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AddressDTO, Address>()
                .ForMember(dst => dst.Users, op => op.Ignore());
            });
            mapper = config.CreateMapper();
            Address address = mapper.Map<AddressDTO, Address>(userDTO.Address);

            user.Address = address;
            return user;
        }

        public List<User> Map(List<UserDTO> usersDTO)
        {
            List<User> users = new List<User>();
            foreach (var userDTO in usersDTO)
            {
                users.Add(Map(userDTO));
            }
            return users;
        }
    }
}
