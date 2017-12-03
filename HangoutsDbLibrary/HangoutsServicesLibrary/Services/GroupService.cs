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
        public Group getByID(int id)
        {
            using (var uow = new UnitOfWork())
            {
                var groupRepository = uow.GetRepository<Group>();
                Group group = groupRepository.GetByID(id);
                return group;

            }
        }

        public List<Group> getAllGroups()
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
                Group existGroup = groupRepository.GetAll().Where(g => g.AdminID == group.AdminID && g.Name == group.Name).FirstOrDefault();
                if (existGroup != null)
                    return null;
                groupRepository.Insert(group);
                uow.SaveChanges();
                return group;
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
                    return null;
                }

                groupToUpdate.Name = group.Name;
                groupRepository.Edit(groupToUpdate);
                uow.SaveChanges();
                return groupToUpdate;
            }
        }

        public Group DeleteGroup(int id)
        {
            using (var uow = new UnitOfWork())
            {
                var groupRepository = uow.GetRepository<Group>();
                Group group = groupRepository.GetByID(id);
                if (group == null)
                {
                    return null;
                }
                groupRepository.Delete(group);
                uow.SaveChanges();
                return group;
            }
        }
    }
}
