using HangoutsDbLibrary.Model;
using HangoutsDbLibrary.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HangoutsBusinessLibrary.Services
{
    public class PlanService
    {
        public Plan GetByID(int id)
        {
            using (var uow = new UnitOfWork())
            {
                var planRepository = uow.GetRepository<Plan>();
                var addressRepository = uow.GetRepository<Address>();
                Plan plan = planRepository.GetAll().Include(p => p.Activity).Where(p => p.ID == id).FirstOrDefault();
                if(plan != null)
                {
                        plan.Address = addressRepository.GetByID(plan.AddressID);
                }
                return plan;
            }
        }

        public List<Plan> GetAllPlans()
        {
            using (var uow = new UnitOfWork())
            {
                var planRepository = uow.GetRepository<Plan>();
                List<Plan> plans = planRepository.GetAll().Include(p => p.Activity).Include(p => p.Address).ToList();
                return plans;
            }
        }

        public Plan AddPlan(Plan plan)
        {
            using (var uow = new UnitOfWork())
            {
                var planRepository = uow.GetRepository<Plan>();
                var addressRepository = uow.GetRepository<Address>();

                var existPlans = planRepository.GetAll().ToList();
                Plan existPlan = null;
                if (existPlans != null || existPlans.Count != 0) 
                   existPlan = existPlans.Where(p => p.Address == plan.Address && p.Group == plan.Group &&
                    p.Activity == plan.Activity).FirstOrDefault();
                if (existPlan != null)
                {
                    throw new Exception("There already exist a similar plan");
                }
                Address existAddress = addressRepository.GetAll().Where(u => u.Latitude == plan.Address.Latitude || u.Longitude == plan.Address.Longitude).FirstOrDefault();
                if (existAddress != null)
                    plan.Address = existAddress;
                plan.Chat = new Chat();
                planRepository.Insert(plan);
                uow.SaveChanges();
                return plan;
            }
        }

        public void DeletePlan(int id)
        {
            using (var uow = new UnitOfWork())
            {
                var planRepository = uow.GetRepository<Plan>();
                Plan plan = planRepository.GetByID(id);
                if (plan == null)
                {
                    throw new Exception("Invalid ID");
                }
                planRepository.Delete(plan);
                uow.SaveChanges();
            }
        }

        public List<Plan> GetAllPlansFromGroup(int id)
        {
            using (var uow = new UnitOfWork())
            {
                var planRepository = uow.GetRepository<Plan>();
                var groupRepository = uow.GetRepository<Group>();
                List<Plan> plans = planRepository.GetAll().Include(p => p.Address).Where(p => p.GroupID == id).ToList();
                return plans;
            }
        }

        
        public List<Plan> GetAllPlansOfUser(int id)
        {
            using (var uow = new UnitOfWork())
            {
                var planRepository = uow.GetRepository<Plan>();
                var planUserRepository = uow.GetRepository<PlanUser>();
                List<Plan> plans = new List<Plan>();
                List<PlanUser> planUsers = planUserRepository.GetAll().Include(pu => pu.User).ToList();
                foreach (var pu in planUsers)
                {
                    if (pu.UserID == id)
                    {
                        plans.Add(pu.Plan);
                    }
                }
                return plans;
            }
        }

        public List<Plan> GetSimilarPlans(int groupId, DateTime startTime, DateTime endTime, string activityDescription)
        {
            using (var uow = new UnitOfWork())
            {
                var planRepository = uow.GetRepository<Plan>();
                var activityRepository = uow.GetRepository<Activity>();
                Activity activity = activityRepository.GetAll().Where(a => a.Description.Equals(activityDescription)).FirstOrDefault();
                if (activity == null)
                    return null;
                List<Plan> plans = planRepository
                    .GetAll()
                    .Where(p => p.GroupID == groupId 
                        &&((p.StartTime >= startTime && p.StartTime <= endTime) || (p.EndTime >= startTime && p.EndTime <= endTime))
                        && p.Activity.ID == activity.ID)
                    .ToList();
                return plans;
            }
        }
    }
}
