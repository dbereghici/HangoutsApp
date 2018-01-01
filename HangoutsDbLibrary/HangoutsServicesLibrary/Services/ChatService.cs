using HangoutsDbLibrary.Model;
using HangoutsDbLibrary.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HangoutsBusinessLibrary.Services
{
    public class ChatService
    {
        public Chat GetChatOfFriendship(int id1, int id2)
        {
            using (var uow = new UnitOfWork())
            {
                var friendshipRepository = uow.GetRepository<Friendship>();
                Friendship friendship = friendshipRepository
                    .GetAll()
                    .Include(f => f.Chat)
                    .ThenInclude(c => c.UserChats)
                    .Include(f => f.Chat)
                    .ThenInclude(c => c.Messages)
                    .Where(f => (f.UserID1 == id1 && f.UserID2 == id2) || (f.UserID2 == id1 && f.UserID1 == id2))
                    .FirstOrDefault();
                if (friendship == null)
                    return null;
                Chat chat = friendship.Chat;
                return chat;
            }
        }

        public Chat GetChatOfPlan(int id)
        {
            using (var uow = new UnitOfWork())
            {
                var planRepository = uow.GetRepository<Plan>();
                var chatRepository = uow.GetRepository<Chat>();
                Plan plan = planRepository.GetByID(id);
                if (plan == null)
                    return null;
                Chat chat = chatRepository.GetByID(plan.ChatID);
                return chat;
            }
        }


        public void DeleteChat(int id)
        {
            using (var uow = new UnitOfWork())
            {
                var chatRepository = uow.GetRepository<Chat>();
                Chat chat = chatRepository.GetByID(id);
                if (chat == null)
                {
                    throw new Exception("Invalid ID");
                }
                chatRepository.Delete(chat);
                uow.SaveChanges();
            }
        }
    }
}
