using AutoMapper;
using HangoutsDbLibrary.Model;
using HangoutsWebApi.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangoutsWebApi.Mappings
{
    public class UserGeneralMapper
    {
        public UserGeneralDTO Map(User user)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserGeneralDTO>()
                .ForMember(dst => dst.Address,
                    op => op.MapFrom(g => g.Address.Location));
            });
            IMapper mapper = config.CreateMapper();
            UserGeneralDTO userDTO = mapper.Map<User, UserGeneralDTO>(user);
            return userDTO;
        }

        public List<UserGeneralDTO> Map(List<User> users)
        {
            List<UserGeneralDTO> usersDTO = new List<UserGeneralDTO>();
            foreach (var user in users)
            {
                usersDTO.Add(Map(user));
            }
            return usersDTO;
        }

        public User Map(UserGeneralDTO userDTO)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserGeneralDTO, User>()
                .ForMember(dst => dst.Address, op => op.Ignore())
                .ForMember(dst => dst.Age, op => op.Ignore())
                .ForMember(dst => dst.FirstName, op => op.Ignore())
                .ForMember(dst => dst.LastName, op => op.Ignore())
                .ForMember(dst => dst.BirthDate, op => op.Ignore())
                .ForMember(dst => dst.Email, op => op.Ignore())
                .ForMember(dst => dst.Password, op => op.Ignore());
            });
            IMapper mapper = config.CreateMapper();
            User user = mapper.Map<UserGeneralDTO, User>(userDTO);
            return user;
        }

        public List<User> Map(List<UserGeneralDTO> usersDTO)
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
