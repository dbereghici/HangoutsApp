using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangoutsWebApi.DTOModels
{
    public class PlanMatchInputDTO
    {
        public int UserID { get; set; }
        public int GroupID { get; set; }
        public string ActivityDescription { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
