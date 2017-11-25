using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangoutsWebApi.DTOModels
{
    public class PlanUserDTO
    {
        public PlanDTO Plan { get; set; }
        public UserDTO User { get; set; }
    }
}
