using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangoutsWebApi.DTOModels
{
    public class ChatDTO
    {
        public int ID { get; set; }
        public List<MessageDTO> Messages { get; set; }
        public List<UserGeneralDTO> Users { get; set; }
    }
}
