using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangoutsWebApi.DTOModels
{
    public class PlanDTO
    {
        public int ID { get; set; }
        public int GroupID { get; set; }
        public GroupDTO Group { get; set; }
        public DateTime CreatedAt { get; set; }
        public int LifeTime { get; set; }
        public AddressDTO Address { get; set; }
        public int AddressID { get; set; }
        public ActivityDTO Activity { get; set; }
    }
}
