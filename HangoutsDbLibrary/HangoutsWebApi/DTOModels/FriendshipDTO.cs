using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangoutsWebApi.DTOModels
{
    public class FriendshipDTO
    {
        public UserDTO User1 { get; set; }
        public UserDTO User2 { get; set; }
    }
}
