using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangoutsWebApi.DTOModels
{
    public class FriendshipDTO
    {
        public UserGeneralDTO User1 { get; set; }
        public UserGeneralDTO User2 { get; set; }
        public int UserID1 { get; set; }
        public int UserID2 { get; set; }
        public string Status { get; set; }
        public int ChatID { get; set; }
    }
}
