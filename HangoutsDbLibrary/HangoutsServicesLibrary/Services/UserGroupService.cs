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
                var groupRepository = uow.GetRepository<UserGroup>();
                var id = new object[] { (object)groupId, (object)userId };
                UserGroup userGroup = groupRepository.GetByID(id);
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

    }
}
