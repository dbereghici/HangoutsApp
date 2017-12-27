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
            try
            {
                friendship = friendshipService.AddFriendship(friendship);
                FriendshipDTO friendshipDTOResponse = friendshipMapper.Map(friendship);
                return Ok(friendshipDTOResponse);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id1}/{id2}")]
        public IActionResult Put(int id1, int id2, [FromBody] FriendshipDTO friendshipDTO)
        {
            FriendshipService friendshipService = new FriendshipService();
            FriendshipMapper friendshipMapper = new FriendshipMapper();
            friendshipDTO.UserID1 = id1;
            friendshipDTO.UserID2 = id2;
            Friendship friendship = friendshipMapper.Map(friendshipDTO);
            try
            {
                friendship = friendshipService.UpdateFriendship(friendship);
                return Ok(friendship);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id1}/{id2}")]
        public IActionResult Delete(int id1, int id2)
        {
            FriendshipService friendshipService = new FriendshipService();
            try
            {
                friendshipService.DeleteFriendship(id1, id2);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("~/api/friends/user/{id}")]
        [HttpGet]
        public IActionResult GetAllFriendsForUser(int id)
        {
            UserService userService = new UserService();
            UserGeneralMapper userGeneralMapper = new UserGeneralMapper();
            User existUser = userService.GetByID(id);
            if (existUser == null)
                return BadRequest("Invalid ID");
            List<User> friends = userService.GetAllFriends(id);
            if (friends == null || friends.Count == 0)
                return NotFound("You have no friends yet.");
            List<UserGeneralDTO> friendsDTO = userGeneralMapper.Map(friends);
            return Ok(friendsDTO);
        }

        [Route("~/api/friends/user/{id}/page/{page}/size/{size}")]
        [HttpGet]
        public IActionResult GetAllFriendsForUserPage(int id, int page, int size)
        {
            UserService userService = new UserService();
            UserGeneralMapper userGeneralMapper = new UserGeneralMapper();
            List<User> source = userService.GetAllFriends(id);
            if (source == null || source.Count == 0)
                return NotFound("You have no friends yet.");

            int count = source.Count;
            int totalPages = (int)Math.Ceiling(count / (double)size);

            if (page > totalPages)
                return BadRequest("Page number out of range");

            List<User> users;

            if ((page - 1) * size + size < count)
                users = source.GetRange((page - 1) * size, size);
            else
                users = source.GetRange((page - 1) * size, count - (page - 1) * size);
            var previousPage = page > 1 ? "Yes" : "No";
            var nextPage = page < totalPages ? "Yes" : "No";
            List<UserGeneralDTO> usersDTO = userGeneralMapper.Map(users);

            var response = new
            {
                totalCount = count,
                pageSize = size,
                currentPage = page,
                totalPages = totalPages,
                previousPage,
                nextPage,
                users = usersDTO
            };

            return Ok(response);
        }

        [Route("~/api/friendrequestmade/user/{id}/page/{page}/size/{size}")]
        [HttpGet]
        public IActionResult GetAllFriendRequestMadeForUserPage(int id, int page, int size)
        {
            FriendshipService friendshipService = new FriendshipService();
            UserGeneralMapper userGeneralMapper = new UserGeneralMapper();
            List<User> source = friendshipService.GetAllFriendRequestsMade(id);
            if (source == null || source.Count == 0)
                return NotFound("You have no friendship requests made");

            int count = source.Count;
            int totalPages = (int)Math.Ceiling(count / (double)size);

            if (page > totalPages)
                return BadRequest("Page number out of range");

            List<User> users;

            if ((page - 1) * size + size < count)
                users = source.GetRange((page - 1) * size, size);
            else
                users = source.GetRange((page - 1) * size, count - (page - 1) * size);
            var previousPage = page > 1 ? "Yes" : "No";
            var nextPage = page < totalPages ? "Yes" : "No";
            List<UserGeneralDTO> usersDTO = userGeneralMapper.Map(users);

            var response = new
            {
                totalCount = count,
                pageSize = size,
                currentPage = page,
                totalPages = totalPages,
                previousPage,
                nextPage,
                users = usersDTO
            };

            return Ok(response);
        }

        [Route("~/api/friendrequestreceived/user/{id}/page/{page}/size/{size}")]
        [HttpGet]
        public IActionResult GetAllFriendRequestReceivedForUserPage(int id, int page, int size)
        {
            FriendshipService friendshipService = new FriendshipService();
            UserGeneralMapper userGeneralMapper = new UserGeneralMapper();
            List<User> source = friendshipService.GetAllFriendRequestReceived(id);
            if (source == null || source.Count == 0)
                return NotFound("You have no friendship requests!");

            int count = source.Count;
            int totalPages = (int)Math.Ceiling(count / (double)size);

            if (page > totalPages)
                return BadRequest("Page number out of range");

            List<User> users;

            if ((page - 1) * size + size < count)
                users = source.GetRange((page - 1) * size, size);
            else
                users = source.GetRange((page - 1) * size, count - (page - 1) * size);
            var previousPage = page > 1 ? "Yes" : "No";
            var nextPage = page < totalPages ? "Yes" : "No";
            List<UserGeneralDTO> usersDTO = userGeneralMapper.Map(users);

            var response = new
            {
                totalCount = count,
                pageSize = size,
                currentPage = page,
                totalPages = totalPages,
                previousPage,
                nextPage,
                users = usersDTO
            };

            return Ok(response);
        }
    }
}

