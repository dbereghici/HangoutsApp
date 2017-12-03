using AutoMapper;
using HangoutsDbLibrary.Model;
using HangoutsWebApi.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangoutsWebApi.Mappings
{
    public class UserGroupMapper
    {
        public UserGroupDTO Map(UserGroup userGroup)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserGroup, UserGroupDTO>();
            });
            IMapper mapper = config.CreateMapper();
            UserGroupDTO userGroupDTO = mapper.Map<UserGroup, UserGroupDTO>(userGroup);
            return userGroupDTO;
        }

        public List<UserGroupDTO> Map(List<UserGroup> userGroups)
        {
            List<UserGroupDTO> userGroupDTOs = new List<UserGroupDTO>();
            foreach (var ug in userGroups)
            {
                userGroupDTOs.Add(Map(ug));
            }
            return userGroupDTOs;
        }

        public UserGroup Map(UserGroupDTO userGroupDTO)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserGroupDTO, UserGroup>();
            });
            IMapper mapper = config.CreateMapper();
            UserGroup userGroup = mapper.Map<UserGroupDTO, UserGroup>(userGroupDTO);
            return userGroup;
        }

        public List<UserGroup> Map(List<UserGroupDTO> usergroupsDTO)
        {
            List<UserGroup> userGroups = new List<UserGroup>();
            foreach (var ug in usergroupsDTO)
            {
                userGroups.Add(Map(ug));
            }
            return userGroups;
        }
    }
}
