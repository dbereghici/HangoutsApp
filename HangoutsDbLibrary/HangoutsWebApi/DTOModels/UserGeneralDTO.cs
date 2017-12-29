using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HangoutsWebApi.DTOModels
{
    public class UserGeneralDTO
    {
        public int ID { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        public int Age { get; set; }
        public string RelationshipStatus { get; set; }
    }
}
