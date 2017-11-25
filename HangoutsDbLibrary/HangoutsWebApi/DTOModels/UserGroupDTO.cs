using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangoutsWebApi.DTOModels
{
    public class UserGroupDTO
    {
        public UserDTO User { get; set; }
        public GroupDTO Group { get; set; }
        public string Status { get; set; }
    }
}
