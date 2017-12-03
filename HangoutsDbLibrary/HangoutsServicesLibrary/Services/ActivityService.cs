using HangoutsDbLibrary.Model;
using HangoutsDbLibrary.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HangoutsBusinessLibrary.Services
{
    public class ActivityService
    {
        public Activity GetByID(int id)
        {
            using (var uow = new UnitOfWork())
            {
                var activityRepository = uow.GetRepository<Activity>();
                Activity activity = activityRepository.GetAll().Include(a => a.Group).ThenInclude(g => g.Admin).Where(a => a.ID == id).FirstOrDefault();
                return activity;
            }
        }

        public List<Activity> GetAllActivities()
        {
            using (var uow = new UnitOfWork())
            {
                var activityRepository = uow.GetRepository<Activity>();
                List<Activity> activities = activityRepository.GetAll().Include(a => a.Group).ThenInclude(g => g.Admin).ToList();
                return activities;
            }
        }

        public Activity AddActivity(Activity activity)
        {
            using (var uow = new UnitOfWork())
            {
                var activityRepository = uow.GetRepository<Activity>();
                var groupRepository = uow.GetRepository<Group>();
                Group group = groupRepository.GetByID(activity.Group.ID);
                if (group == null)
                {
                    return null;
                }
                activity.Group = group;
                Activity existActivity = activityRepository
                    .GetAll()
                    .Where(a => a.Description == activity.Description && a.GroupID == activity.Group.ID)
                    .FirstOrDefault();
                if (existActivity != null)
                    return null;
                activityRepository.Insert(activity);
                uow.SaveChanges();
                return activity;
            }
        }

        public Activity UpdateActivity(Activity activity, int id)
        {
            using (var uow = new UnitOfWork())
            {
                var activityRepository = uow.GetRepository<Activity>();
                Activity activityToUpdate = activityRepository.GetByID(id);
                if (activityToUpdate == null)
                {
                    return null; 
                }
                Activity existActivity = activityRepository.GetAll().Where(a => a.Description == activity.Description).FirstOrDefault();
                if (existActivity != null)
                    return null;
                activityToUpdate.Description = activity.Description;
                activityRepository.Edit(activityToUpdate);
                uow.SaveChanges();
                return activityToUpdate;
            }
        }

        public Activity DeleteActivity(int id)
        {
            using (var uow = new UnitOfWork())
            {
                var activityRepository = uow.GetRepository<Activity>();
                Activity activity = activityRepository.GetByID(id);
                if (activity == null)
                {
                    return null;
                }
                activityRepository.Delete(activity);
                uow.SaveChanges();
                return activity;
            }
        }
    }
}
