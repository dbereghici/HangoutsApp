using AutoMapper;
using HangoutsDbLibrary.Model;
using HangoutsDbLibrary.Repository;
using HangoutsWebApi.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangoutsWebApi.Mappings
{
    public class ChatMapper
    {
        public ChatDTO Map(Chat chat)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Chat, ChatDTO>()
                .ForMember(src => src.Users,
                    op => op.Ignore())
                .ForMember(src => src.Messages,
                    op => op.Ignore());
            });
            IMapper mapper = config.CreateMapper();
            ChatDTO chatDTO = mapper.Map<Chat, ChatDTO>(chat);
            using (var uow = new UnitOfWork())
            {
                var userRepository = uow.GetRepository<User>();
                List<User> users = new List<User>();
                if (chat.UserChats != null)
                    foreach (var uc in chat.UserChats)
                    {
                        users.Add(userRepository.GetByID(uc.UserID));
                    }
                UserGeneralMapper userGeneralMapper = new UserGeneralMapper();
                chatDTO.Users = userGeneralMapper.Map(users);
            }
            MessageMapper messageMapper = new MessageMapper();
            chatDTO.Messages = messageMapper.Map(chat.Messages);
            return chatDTO;
        }

    }
}
