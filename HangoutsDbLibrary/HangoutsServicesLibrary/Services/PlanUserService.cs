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
                //>>
                UserChat userChat = userChatRepository.GetAll().Where(c => c.ChatID == plan.ChatID && c.UserID == planUser.UserID).FirstOrDefault();
                if (userChat == null)
                    userChatRepository.Insert(new UserChat { ChatID = plan.ChatID, UserID = planUser.UserID });
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

        public string getStatus(int planId, int userId)
        {
            using (var uow = new UnitOfWork())
            {
                var planUserRepository = uow.GetRepository<PlanUser>();
                PlanUser planUser = planUserRepository.GetAll().Where(pu => pu.PlanID == planId && pu.UserID == userId).FirstOrDefault();
                if (planUser != null)
                    return "member";
                return "";
            }
        }

        public void DeletePlanUser(int userId, int planId)
        {
            using (var uow = new UnitOfWork())
            {
                var planUserRepository = uow.GetRepository<PlanUser>();
                PlanUser planUser = planUserRepository.GetAll().Where(pu => pu.PlanID == planId && pu.UserID == userId).FirstOrDefault();
                if (planUser != null)
                {
                    planUserRepository.Delete(planUser);
                    uow.SaveChanges();
                    var planRepository = uow.GetRepository<Plan>();
                    Plan plan = planRepository.GetAll().Include(p => p.PlanUsers).Where(p => p.ID == planId).FirstOrDefault();
                    if(plan.PlanUsers.Count == 0)
                    {
                        var chatRepository = uow.GetRepository<Chat>();
                        var userChatRep = uow.GetRepository<UserChat>();
                        Chat chat = chatRepository.GetAll().Where(c => c.ID == plan.ChatID).Include(c => c.UserChats).FirstOrDefault();
                        foreach (var uc in chat.UserChats)
                            userChatRep.Delete(uc);
                        chatRepository.Delete(chat);
                        planRepository.Delete(plan);
                    }
                    uow.SaveChanges();
                }
            }
        }
    }
}
