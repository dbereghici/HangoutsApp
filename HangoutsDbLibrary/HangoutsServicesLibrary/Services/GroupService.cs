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

        public Group AddGroup(Group group)
        {
            using (var uow = new UnitOfWork())
            {
                var groupRepository = uow.GetRepository<Group>();
                var userGroupRepository = uow.GetRepository<UserGroup>();
                Group existGroup = groupRepository.GetAll().Where(g => g.AdminID == group.AdminID && g.Name == group.Name).FirstOrDefault();
                if (existGroup != null)
                    throw new Exception("There already exist a group with this name");
                groupRepository.Insert(group);
                userGroupRepository.Insert(new UserGroup { UserID = group.AdminID, GroupID = group.ID, Status = "admin" });
                uow.SaveChanges();
                return group;
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

        public Group UpdateGroup(Group group, int id)
        {
            using (var uow = new UnitOfWork())
            {
                var groupRepository = uow.GetRepository<Group>();
                Group groupToUpdate = groupRepository.GetByID(id);
                if (groupToUpdate == null)
                {
                    throw new Exception("Invalid ID");
                }
                Group existGroup = groupRepository.GetAll().Where(g => g.AdminID == group.AdminID && g.Name == group.Name).FirstOrDefault();
                if (existGroup != null)
                    throw new Exception("There already exist a group with this name");
                groupToUpdate.Name = group.Name;
                groupRepository.Edit(groupToUpdate);
                uow.SaveChanges();
                return groupToUpdate;
            }
        }

        public void DeleteGroup(int id)
        {
            using (var uow = new UnitOfWork())
            {
                var groupRepository = uow.GetRepository<Group>();
                Group group = groupRepository.GetByID(id);
                if (group == null)
                {
                    throw new Exception("Invalid ID");
                }
                groupRepository.Delete(group);
                uow.SaveChanges();
            }
        }

        public List<Group> GetAllGroups(string searchString)
        {
            using (var uow = new UnitOfWork())
            {
                var groupRepository = uow.GetRepository<Group>();

                List<Group> groups = new List<Group>();
                List<Group> intermedGroups = new List<Group>();

                if (searchString == null || searchString.Equals(""))
                    groups = groupRepository
                        .GetAll()
                        .Include(g => g.UserGroups)
                        .ToList();
                else
                {
                    char[] delimiterChar = { ' ' };
                    string[] words = searchString.Split(delimiterChar);

                    foreach (var word in words)
                    {
                        intermedGroups = groupRepository
                            .GetAll()
                            .Where(g => g.Name.Contains(word))
                            .ToList();
                        groups = groups.Concat(intermedGroups).ToList();
                    }
                }
                return groups;
            }
        }

        public List<Group> GetMyGroups(int id, string searchString)
        {
            using (var uow = new UnitOfWork())
            {
                var userRepository = uow.GetRepository<User>();
                var groupRepository = uow.GetRepository<Group>();
                var userGroupRepository = uow.GetRepository<UserGroup>();
                User user = userRepository.GetByID(id);
                if (user == null)
                    throw new Exception("Invalid id");
                List<UserGroup> usergroups = userGroupRepository.GetAll().ToList();
                List<Group> groups = GetAllGroups(searchString);
                List<Group> result = new List<Group>();
                foreach (var group in groups)
                {
                    foreach (var ug in group.UserGroups)
                        if (ug.UserID == id)
                            result.Add(group);
                }
                return result;
            }
        }

        public List<Group> GetGroupsAdministrated(int id, string searchString)
        {
            using (var uow = new UnitOfWork())
            {
                var userRepository = uow.GetRepository<User>();
                var groupRepository = uow.GetRepository<Group>();
                var userGroupRepository = uow.GetRepository<UserGroup>();
                User user = userRepository.GetByID(id);
                if (user == null)
                    throw new Exception("Invalid id");
                List<UserGroup> usergroups = userGroupRepository.GetAll().ToList();
                List<Group> groups = GetAllGroups(searchString);
                List<Group> result = new List<Group>();
                foreach (var group in groups)
                {
                    if (group.AdminID == id)
                        result.Add(group);

                }
                return result;
            }
        }
    }
}
