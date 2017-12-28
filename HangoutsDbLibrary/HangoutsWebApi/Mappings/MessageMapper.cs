using AutoMapper;
using HangoutsDbLibrary.Model;
using HangoutsWebApi.DTOModels;
using HangoutsWebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangoutsWebApi.Mappings
{
    public class MessageMapper
    {
        public MessageDTO Map(Message message)
        {
            var config = new MapperConfiguration(cfg => { 
                cfg.CreateMap<Message, MessageDTO>()
                .ForMember(src => src.User, op => op.Ignore());
            });
            IMapper mapper = config.CreateMapper();
            MessageDTO messageDTO = mapper.Map<Message, MessageDTO>(message);
            UserService userService = new UserService();
            UserGeneralMapper userGeneralMapper = new UserGeneralMapper();
            User user = userService.GetByID(message.UserID);
            messageDTO.User = user.FirstName + " " + user.LastName;
            return messageDTO;
        }

        public List<MessageDTO> Map(List<Message> messages)
        {
            List<MessageDTO> messagesDTO = new List<MessageDTO>();
            foreach (var m in messages)
            {
                messagesDTO.Add(Map(m));
            }
            return messagesDTO;
        }

        public Message Map(MessageDTO messageDTO)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MessageDTO, Message>();
            });
            IMapper mapper = config.CreateMapper();
            Message message = mapper.Map<MessageDTO, Message>(messageDTO);
            return message;
        }

        public List<Message> Map(List<MessageDTO> messagesDTO)
        {
            List<Message> messages = new List<Message>();
            foreach (var m in messagesDTO)
            {
                messages.Add(Map(m));
            }
            return messages;
        }
    }
}
