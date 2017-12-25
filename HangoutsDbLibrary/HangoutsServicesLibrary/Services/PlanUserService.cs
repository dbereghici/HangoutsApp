using HangoutsDbLibrary.Model;
using HangoutsDbLibrary.Repository;
using HangoutsWebApi.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HangoutsBusinessLibrary.Services
{
    public class PlanUserService
    {
        public PlanUser AddPlanUser(PlanUser planUser)
        {
            PlanService planService = new PlanService();
            UserService userService = new UserService();
            Plan plan = planService.GetByID(planUser.PlanID);
            if (plan == null)
                throw new Exception("Invalid plan ID");
            User user = userService.GetByID(planUser.UserID);
            if (user == null)
                throw new Exception("Invalid user ID");
            using (var uow = new UnitOfWork())
            {
                var planUserRepository = uow.GetRepository<PlanUser>();
                var userChatRepository = uow.GetRepository<UserChat>();
                PlanUser existPlanUser = planUserRepository
                    .GetAll()
                    .Where(pu => pu.PlanID == planUser.PlanID && pu.UserID == planUser.UserID)
                    .FirstOrDefault();
                if (existPlanUser != null)
                    throw new Exception("This user is already in this plan");
                planUserRepository.Insert(planUser);
                
                userChatRepository.Insert(new UserChat { ChatID = plan.ChatID, UserID = planUser.UserID});
                uow.SaveChanges();
                return planUser;
            }
        }

        public List<User> GetAllUsersFromAPlan(int planId)
        {
            using (var uow = new UnitOfWork())
            {
                var planUserRepository = uow.GetRepository<PlanUser>();
                List<User> users = new List<User>();
                List<PlanUser> planUsers = planUserRepository.GetAll().Include(pu => pu.User).ToList();

                foreach (var pu in planUsers)
                {
                    if (pu.PlanID == planId)
                    {
                        users.Add(pu.User);
                    }
                }
                return users;
            }
        }
    }
}
