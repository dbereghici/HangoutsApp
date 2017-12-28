using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HangoutsWebApi.DTOModels
{
    public class MessageDTO
    {
        public int ID { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        //public UserGeneralDTO User { get; set; }
        public string User { get; set; }
        [Required]
        public int ChatID { get; set; }
        [Required]
        public int UserID { get; set; }
    }
}
