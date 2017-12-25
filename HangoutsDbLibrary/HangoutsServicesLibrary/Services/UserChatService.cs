using HangoutsDbLibrary.Migrations;
using HangoutsDbLibrary.Model;
using HangoutsDbLibrary.Repository;
using HangoutsWebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HangoutsBusinessLibrary.Services
{
    public class UserChatService
    {
        public UserChat AddUserChat(UserChat userChat)
        {
            UserService userService = new UserService();
            using (var uow = new UnitOfWork())
            {
                var chatRepository = uow.GetRepository<Chat>();
                var userRepository = uow.GetRepository<User>();
                var userChatRepository = uow.GetRepository<UserChat>();
                Chat chat = chatRepository.GetByID(userChat.ChatID);
                if (chat == null)
                    throw new Exception("Invalid chat ID");
                User user = userRepository.GetByID(userChat.UserID);
                if (user == null)
                    throw new Exception("Invalid user ID");
                UserChat existUserChat = userChatRepository
                    .GetAll()
                    .Where(uc => uc.ChatID == userChat.ChatID && uc.UserID == userChat.UserID)
                    .FirstOrDefault();
                if (existUserChat != null)
                    throw new Exception("This user is already in this chat");
                userChatRepository.Insert(userChat);
                uow.SaveChanges();
                return userChat;
            }
        }
    }
}
