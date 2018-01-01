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
                Group group = groupRepository.GetByID(activity.GroupID);
                activity.Group = group ?? throw new Exception("Invalid group ID");
                Activity existActivity = activityRepository
                    .GetAll()
                    .Where(a => a.Description == activity.Description && a.GroupID == activity.Group.ID)
                    .FirstOrDefault();
                if (existActivity != null)
                    throw new Exception("There is already an activity with such a description");
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
                    throw new Exception("Invalid ID");
                Activity existActivity = activityRepository.GetAll().Where(a => a.Description == activity.Description).FirstOrDefault();
                if (existActivity == null)
                {
                    throw new Exception("There is already an activity with such a description");
                }
                activityToUpdate.Description = activity.Description;
                activityRepository.Edit(activityToUpdate);
                uow.SaveChanges();
                return activityToUpdate;
            }
        }

        public void DeleteActivity(int id)
        {
            using (var uow = new UnitOfWork())
            {
                var planRepository = uow.GetRepository<Plan>();
                List<Plan> plan = planRepository.GetAll().Where(p => p.ActivityID == id).ToList();
                if (plan != null)
                    throw new Exception("This activity can not be deleted as long as there are plans containing it");
                var activityRepository = uow.GetRepository<Activity>();
                Activity activity = activityRepository.GetByID(id);
                if (activity == null)
                {
                    throw new Exception("Invalid ID");
                }
                activityRepository.Delete(activity);
                uow.SaveChanges();
            }
        }

        public Activity GetActivityByDescription(string description)
        {
            using (var uow = new UnitOfWork())
            {
                var activityRepository = uow.GetRepository<Activity>();
                Activity activity = activityRepository.GetAll().Where(a => a.Description == description).FirstOrDefault();
                return activity;
            }
        }
    }
}
