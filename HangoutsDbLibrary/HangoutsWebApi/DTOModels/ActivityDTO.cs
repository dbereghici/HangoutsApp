using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangoutsWebApi.DTOModels
{
    public class ActivityDTO
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public GroupDTO GroupDTO { get; set; }
    }
}
