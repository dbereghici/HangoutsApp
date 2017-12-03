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
        public int ActivityID { get; set; }
        public int ChatID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public AddressDTO Address { get; set; }
    }
}
