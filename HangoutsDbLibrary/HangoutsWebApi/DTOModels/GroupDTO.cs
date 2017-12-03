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
        public UserGeneralDTO Admin { get; set; }
    }
}
