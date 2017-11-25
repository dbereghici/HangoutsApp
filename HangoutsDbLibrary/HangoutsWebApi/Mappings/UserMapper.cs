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
        public UserDTO MapToDTO(User user)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>();
            });
            IMapper mapper = config.CreateMapper();
            return mapper.Map<User, UserDTO>(user);
        }

        public User MapFromDTO(UserDTO userDTO)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDTO, User>();
            });
            IMapper mapper = config.CreateMapper();
            return mapper.Map<UserDTO, User>(userDTO);
        }
    }
}
