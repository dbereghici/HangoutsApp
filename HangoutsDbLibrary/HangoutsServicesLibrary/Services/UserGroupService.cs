using HangoutsDbLibrary.Model;
using HangoutsDbLibrary.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangoutsWebApi.Services
{
    public class UserGroupService
    {
        public UserGroup GetByID(int groupId, int userId)
        {
            using (var uow = new UnitOfWork())
            {
                var userGroupRepository = uow.GetRepository<UserGroup>();
                var id = new object[] { (object)groupId, (object)userId };
                UserGroup userGroup = userGroupRepository.GetByID(id);
                return userGroup;

            }
        }

        public List<UserGroup> GetAllUserGroups()
        {
            using (var uow = new UnitOfWork())
            {
                var userGroupRepository = uow.GetRepository<UserGroup>();
                List<UserGroup> userGroups = userGroupRepository.GetAll().ToList();
                return userGroups;
            }
        }

        public UserGroup AddUserGroup(UserGroup userGroup)
        {
            UserService userService = new UserService();
            GroupService groupService = new GroupService();
            Group group = groupService.GetByID(userGroup.GroupID);
            if (group == null)
                throw new Exception("Invalid group ID");
            User user = userService.GetByID(userGroup.UserID);
            if (user == null)
                throw new Exception("Invalid user ID");
            using (var uow = new UnitOfWork())
            {
                var userGroupRepository = uow.GetRepository<UserGroup>();
                UserGroup existUserGroup = userGroupRepository
                    .GetAll()
                    .Where(ug => ug.GroupID == userGroup.GroupID && ug.UserID == userGroup.UserID)
                    .FirstOrDefault();
                if (existUserGroup != null)
                    throw new Exception("This user is already in this group");
                userGroupRepository.Insert(userGroup);
                uow.SaveChanges();
                return userGroup;
            }
        }

        public List<User> GetAllUsersFromAGroup(int groupId)
        {
            using (var uow = new UnitOfWork())
            {
                var userGroupRepository = uow.GetRepository<UserGroup>();
                List<User> users = new List<User>();
                List<UserGroup> userGroups = userGroupRepository.GetAll().Include(ug => ug.User).ToList();

                foreach (var ug in userGroups)
                {
                    if (ug.GroupID == groupId)
                    {
                        users.Add(ug.User);
                    }
                }
                return users;
            }
        }

        public void DeleteUserGroup(int userId, int groupId)
        {
            using (var uow = new UnitOfWork())
            {
                var userGroupRepository = uow.GetRepository<UserGroup>();
                UserGroup userGroup = userGroupRepository.GetAll().Where(ug => ug.GroupID == groupId && ug.UserID == userId).FirstOrDefault();
                if (userGroup == null)
                    throw new Exception(userId + " is not member of " + groupId);
                userGroupRepository.Delete(userGroup);
                uow.SaveChanges();
            }
        }

        public UserGroup UpdateUserGroup(UserGroup userGroup)
        {
            UserService userService = new UserService();
            GroupService groupService = new GroupService();
            Group group = groupService.GetByID(userGroup.GroupID);
            if (group == null)
                throw new Exception("Invalid group ID");
            User user = userService.GetByID(userGroup.UserID);
            if (user == null)
                throw new Exception("Invalid user ID");
            using (var uow = new UnitOfWork())
            {
                var userGroupRepository = uow.GetRepository<UserGroup>();
                UserGroup userGroupToUpdate = userGroupRepository
                    .GetAll()
                    .Where(ug => ug.GroupID == userGroup.GroupID && ug.UserID == userGroup.UserID)
                    .FirstOrDefault();
                if (userGroupToUpdate == null)
                    throw new Exception("This user is not member of this group");
                userGroupToUpdate.Status = userGroup.Status;
                userGroupRepository.Edit(userGroupToUpdate);
                uow.SaveChanges();
                return userGroupToUpdate;
            }
        }
    }
}
