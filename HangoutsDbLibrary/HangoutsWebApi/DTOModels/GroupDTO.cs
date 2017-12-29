using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangoutsWebApi.DTOModels
{
    public class GroupDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Admin { get; set; }
        public int AdminID { get; set; }
        public int NrOfMembers { get; set; }
        public string Status { get; set; }
    }
}
