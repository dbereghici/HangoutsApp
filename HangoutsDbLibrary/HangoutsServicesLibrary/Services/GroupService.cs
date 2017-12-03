using HangoutsDbLibrary.Model;
using HangoutsDbLibrary.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangoutsWebApi.Services
{
    public class GroupService
    {
        public Group GetByID(int id)
        {
            using (var uow = new UnitOfWork())
            {
                var groupRepository = uow.GetRepository<Group>();
                Group group = groupRepository.GetByID(id);
                return group;

            }
        }

        public List<Group> GetAllGroups()
        {
            using (var uow = new UnitOfWork())
            {
                var groupRepository = uow.GetRepository<Group>();
                List<Group> groups = groupRepository.GetAll().Include(g => g.Admin).ThenInclude(u => u.Address).ToList();
                return groups;
            }
        }

        public string AddGroup(Group group)
        {
            using (var uow = new UnitOfWork())
            {
                var groupRepository = uow.GetRepository<Group>();
                var userGroupRepository = uow.GetRepository<UserGroup>();
                Group existGroup = groupRepository.GetAll().Where(g => g.AdminID == group.AdminID && g.Name == group.Name).FirstOrDefault();
                if (existGroup != null)
                    return "There already exist a group with this name";
                groupRepository.Insert(group);
                userGroupRepository.Insert(new UserGroup { UserID = group.AdminID, GroupID = group.ID });
                uow.SaveChanges();
                return "Ok";
            }
        }

        public List<Group> GetAllGroupOfUser(int userId)
        {
            using (var uow = new UnitOfWork())
            {
                var groupRepository = uow.GetRepository<Group>();
                var userGroupRepository = uow.GetRepository<UserGroup>();
                List<Group> groups = new List<Group>();
                List<UserGroup> userGroups = userGroupRepository
                    .GetAll()
                    .Include(ug => ug.Group)
                    .ThenInclude(g => g.Admin)
                    .ToList();
                foreach (var ug in userGroups)
                {
                    if (ug.UserID == userId)
                    {
                        groups.Add(ug.Group);
                    }
                }
                return groups;
            }
        }

        public string UpdateGroup(Group group, int id)
        {
            using (var uow = new UnitOfWork())
            {
                var groupRepository = uow.GetRepository<Group>();
                Group groupToUpdate = groupRepository.GetByID(id);
                if (groupToUpdate == null)
                {
                    return "Invalid ID";
                }
                Group existGroup = groupRepository.GetAll().Where(g => g.AdminID == group.AdminID && g.Name == group.Name).FirstOrDefault();
                if (existGroup != null)
                    return "There already exist a group with this name";
                groupToUpdate.Name = group.Name;
                groupRepository.Edit(groupToUpdate);
                uow.SaveChanges();
                return "Ok";
            }
        }

        public string DeleteGroup(int id)
        {
            using (var uow = new UnitOfWork())
            {
                var groupRepository = uow.GetRepository<Group>();
                Group group = groupRepository.GetByID(id);
                if (group == null)
                {
                    return "Invalid ID";
                }
                groupRepository.Delete(group);
                uow.SaveChanges();
                return "Ok";
            }
        }
    }
}
