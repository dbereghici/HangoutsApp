using AutoMapper;
using HangoutsDbLibrary.Model;
using HangoutsDbLibrary.Repository;
using HangoutsWebApi.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangoutsWebApi.Mappings
{
    public class GroupMapper
    {
        public GroupDTO Map (Group group)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Group, GroupDTO>();
            });
            IMapper mapper = config.CreateMapper();
            GroupDTO groupDTO = mapper.Map<Group, GroupDTO>(group);
            UserGeneralMapper userGeneralMapper = new UserGeneralMapper();
            using (var uow = new UnitOfWork())
            {
                var userRepository = uow.GetRepository<User>();
                User user = userRepository.GetByID(group.AdminID);
                groupDTO.Admin = user.FirstName + " " + user.LastName;
                groupDTO.NrOfMembers = group.UserGroups.Count;
            }
            return groupDTO;
        }

        public List<GroupDTO> Map (List<Group> groups)
        {
            List<GroupDTO> groupsDTO = new List<GroupDTO>();
            foreach (var group in groups)
            {
                groupsDTO.Add(Map(group));
            }
            return groupsDTO;
        }

        public Group Map(GroupDTO groupDTO)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GroupDTO, Group>()
                .ForMember(dst => dst.Admin, 
                    op => op.Ignore());
            });
            IMapper mapper = config.CreateMapper();
            Group group = mapper.Map<GroupDTO, Group>(groupDTO);
            UserGeneralMapper userGeneralMapper = new UserGeneralMapper();
            //group.Admin = userGeneralMapper.Map(groupDTO.Admin);
            return group;
        }

        public List<Group> Map(List<GroupDTO> groupsDTO)
        {
            List<Group> groups = new List<Group>();
            foreach (var groupDTO in groupsDTO)
            {
                groups.Add(Map(groupDTO));
            }
            return groups;
        }
    }
}
