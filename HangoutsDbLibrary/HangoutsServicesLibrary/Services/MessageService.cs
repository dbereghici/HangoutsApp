using HangoutsDbLibrary.Model;
using HangoutsDbLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HangoutsBusinessLibrary.Services
{
    public class MessageService
    {
        public Message AddMessage(Message message)
        {
            using (var uow = new UnitOfWork())
            {
                var messageRepository = uow.GetRepository<Message>();
                if (message.Content == null || message.Content == "")
                    return null;
                messageRepository.Insert(message);
                uow.SaveChanges();
                return message;
            }

        }
    }
}
