using HangoutsDbLibrary.Model;
using HangoutsDbLibrary.Repository;
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
                List<Group> groups = groupRepository.GetAll().ToList();
                return groups;
            }
        }
    }
}
