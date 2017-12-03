using AutoMapper;
using HangoutsDbLibrary.Model;
using HangoutsWebApi.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangoutsWebApi.Mappings
{
    public class FriendshipMapper
    {
        public FriendshipDTO Map (Friendship friendship)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Friendship, FriendshipDTO>();
            });
            IMapper mapper = config.CreateMapper();
            FriendshipDTO friendshipDTO = mapper.Map<Friendship, FriendshipDTO>(friendship);
            return friendshipDTO;
        }

        public List<FriendshipDTO> Map (List<Friendship> friendships)
        {
            List<FriendshipDTO> friendshipDTOs = new List<FriendshipDTO>();
            foreach (var f in friendships)
            {
                friendshipDTOs.Add(Map(f));
            }
            return friendshipDTOs;
        }

        public Friendship Map(FriendshipDTO friendshipDTO)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FriendshipDTO, Friendship>();
            });
            IMapper mapper = config.CreateMapper();
            Friendship friendship = mapper.Map<FriendshipDTO, Friendship>(friendshipDTO);
            return friendship;
        }

        public List<Friendship> Map(List<FriendshipDTO> friendshipsDTO)
        {
            List<Friendship> friendships = new List<Friendship>();
            foreach (var f in friendshipsDTO)
            {
                friendships.Add(Map(f));
            }
            return friendships;
        }
    }
}
