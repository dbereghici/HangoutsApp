using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HangoutsBusinessLibrary.Services;
using HangoutsWebApi.Services;
using HangoutsWebApi.Mappings;
using HangoutsWebApi.DTOModels;
using HangoutsDbLibrary.Model;

namespace HangoutsWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Friendship")]
    public class FriendshipController : Controller
    {
        [HttpGet]
        public IActionResult GetAllFriendships()
        {
            FriendshipService friendshipService = new FriendshipService();
            FriendshipMapper friendshipMapper = new FriendshipMapper();
            List<Friendship> friendships = friendshipService.GetAllFriendships();
            List<FriendshipDTO> friendshipsDTO = friendshipMapper.Map(friendships);
            if (friendshipsDTO == null || friendshipsDTO.Count == 0)
                return NotFound("There are not any friendship relations");
            return Ok(friendshipsDTO);
        }

        [HttpGet("{id1}/{id2}")]
        public IActionResult GetByID(int id1, int id2)
        {
            FriendshipService friendshipService = new FriendshipService();
            FriendshipMapper friendshipMapper = new FriendshipMapper();
            Friendship friendship = friendshipService.GetByID(id1, id2);
            if (friendship == null)
                return BadRequest("Invalid ID");
            FriendshipDTO friendshipDTO = friendshipMapper.Map(friendship);
            return Ok(friendshipDTO);
        }

        [HttpPost]
        public IActionResult Post([FromBody] FriendshipDTO friendshipDTO)
        {
            FriendshipService friendshipService = new FriendshipService();
            FriendshipMapper friendshipMapper = new FriendshipMapper();
            Friendship friendship = friendshipMapper.Map(friendshipDTO);
            string res = friendshipService.AddFriendship(friendship);
            if (res.Equals("Ok"))
                return Ok();
            else
                return BadRequest(res);
        }

        [HttpPut("{id1}/{id2}")]
        public IActionResult Put(int id1, int id2, [FromBody] FriendshipDTO friendshipDTO)
        {
            FriendshipService friendshipService = new FriendshipService();
            FriendshipMapper friendshipMapper = new FriendshipMapper();
            friendshipDTO.UserID1 = id1;
            friendshipDTO.UserID2 = id2;
            Friendship friendship = friendshipMapper.Map(friendshipDTO);
            Friendship existFriendship = friendshipService.GetByID(id1, id2);
            string res;
            if(existFriendship == null)
                res = friendshipService.AddFriendship(friendship);
            else
                res = friendshipService.UpdateFriendship(friendship);
            if (res.Equals("Ok"))
                return Ok();
            else
                return BadRequest(res);
        }

        [HttpDelete("{id1}/{id2}")]
        public IActionResult Delete(int id1, int id2)
        {
            FriendshipService friendshipService = new FriendshipService();
            string res = friendshipService.DeleteFriendship(id1, id2);
            if (res.Equals("Ok"))
                return Ok();
            else
                return BadRequest(res);
        }
    }
}