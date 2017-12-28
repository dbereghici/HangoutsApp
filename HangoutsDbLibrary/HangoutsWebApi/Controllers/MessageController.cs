using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HangoutsWebApi.DTOModels;
using HangoutsBusinessLibrary.Services;
using HangoutsWebApi.Mappings;
using HangoutsDbLibrary.Model;

namespace HangoutsWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Message")]
    public class MessageController : Controller
    {
        [HttpPost]
        public IActionResult Post([FromBody] MessageDTO messageDTO)
        {
            if (ModelState.IsValid)
            {
                MessageService messageService = new MessageService();
                MessageMapper messageMapper = new MessageMapper();
                Message message = messageMapper.Map(messageDTO);
                message.CreatedAt = DateTime.Now;
                message = messageService.AddMessage(message);
                return Ok();
            }
            else
            {
                return BadRequest("The model is not valid!");
            }
        }
    }
}