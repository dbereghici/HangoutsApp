using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HangoutsWebApi.Services;
using HangoutsWebApi.Mappings;
using HangoutsDbLibrary.Model;
using HangoutsWebApi.DTOModels;
using HangoutsBusinessLibrary.Services;

namespace HangoutsWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Chat")]
    public class ChatController : Controller
    {
        [HttpGet("friendship")]
        public IActionResult GetChatOfFriendship(int id1, int id2)
        {
            ChatService chatService = new ChatService();
            ChatMapper chatMapper = new ChatMapper();
            Chat chat = chatService.GetChatOfFriendship(id1, id2);
            if (chat == null)
                return NotFound();
            ChatDTO chatDTO = chatMapper.Map(chat);
            return Ok(chatDTO);
        }
    }
}