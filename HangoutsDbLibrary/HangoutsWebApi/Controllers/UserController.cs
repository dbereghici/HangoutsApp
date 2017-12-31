using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HangoutsDbLibrary.Model;
using HangoutsWebApi.Services;
using HangoutsWebApi.DTOModels;
using AutoMapper;
using HangoutsWebApi.Mappings;
using System.ComponentModel.DataAnnotations;
using HangoutsBusinessLibrary.Services;
using System;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace HangoutsWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            UserService userService = new UserService();
            UserGeneralMapper userGeneralMapper = new UserGeneralMapper();
            List<User> users = userService.GetAllUsers();
            if (users == null || users.Count == 0)
            {
                return NotFound("There are no users!");
            }
            List<UserGeneralDTO> usersDTO = userGeneralMapper.Map(users);
            return Ok(usersDTO);
        }

        [HttpGet("{id}/friends/search")]
        public IActionResult SearchNewFriends(int id, string q, int page, int size) {
            UserService userService = new UserService();
            UserGeneralMapper userGeneralMapper = new UserGeneralMapper();
            List<User> source = userService.SearchNewFriends(id, q);
            if (source == null || source.Count == 0)
            {
                return NotFound("We couldn't find anything for " + q);
            }
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

        [HttpGet("{id}/search")]
        public IActionResult SearchAllFriends(int id, string q, int page, int size)
        {
            UserService userService = new UserService();
            UserGeneralMapper userGeneralMapper = new UserGeneralMapper();
            List<User> source = userService.SearchAllFriends(id, q);
            if (source == null || source.Count == 0)
            {
                return NotFound("We couldn't find anything for " + q);
            }
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

        [HttpGet("page/{page}/size/{size}")]
        public IActionResult GetAllUsersPage(int page, int size)
        {
            UserService userService = new UserService();
            UserGeneralMapper userGeneralMapper = new UserGeneralMapper();
            List<User> source = userService.GetAllUsers();
            if (source == null || source.Count == 0)
            {
                return NotFound("There are no users!");
            }
            int count = source.Count;
            int totalPages = (int)Math.Ceiling(count / (double)size);

            if (page > totalPages)
                return BadRequest("Page number out of range");

            List<User> users;
            if((page - 1) * size + size < count)
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

        [HttpGet("{id}/page/{page}/size/{size}")]
        public IActionResult GetAllUsersWithRelationStatusPage(int id, int page, int size)
        {
            UserService userService = new UserService();
            FriendshipService friendshipService = new FriendshipService();
            UserGeneralMapper userGeneralMapper = new UserGeneralMapper();
            List<User> source = userService.GetAllUsersExceptId(id);
            if (source == null || source.Count == 0)
            {
                return NotFound("There are no users!");
            }
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

            foreach(var u in usersDTO)
            {
                u.RelationshipStatus = friendshipService.GetUserRelation(u.ID, id);
            }

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

        [HttpGet("{id}")]
        public IActionResult GetUserByID(int id)
        {
            UserService userService = new UserService();
            User user = userService.GetByID(id);
            UserGeneralMapper userGeneralMapper = new UserGeneralMapper();
            UserGeneralDTO userGeneralDTO = userGeneralMapper.Map(user);
            if (user == null)
            {
                return NotFound("There is not user with such an ID");
            }
            return Ok(userGeneralDTO);
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                UserService userService = new UserService();
                UserMapper userMapper = new UserMapper();
                User user = userMapper.Map(userDTO);
                try
                {
                    user = userService.AddUser(user);
                    UserDTO userDTOResponse = userMapper.Map(user);
                    return Ok(userDTOResponse);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            else
            {
                return BadRequest("The model is not valid!");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserDTO userDTO)
        {
            UserService userService = new UserService();
            UserMapper userMapper = new UserMapper();
            if (!ModelState.IsValid)
            {
                return BadRequest("The model is not valid!");
            }
            User user = userMapper.Map(userDTO);
            try
            {

                user = userService.UpdateUser(id, user);
                UserDTO userDTOResponse = userMapper.Map(user);
                return Ok(userDTOResponse);
            } 
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            UserService userService = new UserService();
            try
            {
                userService.DeleteUser(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("group")]
        public IActionResult AddUserToGroup([FromBody] UserGroupDTO userGroupDTO)
        {
            UserGroupService userGroupService = new UserGroupService();
            UserGroupMapper userGroupMapper = new UserGroupMapper();
            UserGroup userGroup = userGroupMapper.Map(userGroupDTO);
            try
            {
                userGroup = userGroupService.AddUserGroup(userGroup);
                return Ok(userGroup);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("plan")]
        public IActionResult AddUserToPlan([FromBody] PlanUserDTO planUserDTO)
        {
            PlanUserService planUserService = new PlanUserService();
            PlanUser planUser = new PlanUser { PlanID = planUserDTO.PlanID, UserID = planUserDTO.UserID };
            try
            {
                planUser = planUserService.AddPlanUser(planUser);
                return Ok(planUser);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet()]
        [Route("group/{id}")]
        public IActionResult GetAllUsersFromAGroup(int id)
        {
            GroupService groupService = new GroupService();
            if (groupService.GetByID(id) == null)
                return NotFound("Invalid ID");
            UserGroupService userGroupService = new UserGroupService();
            UserGeneralMapper userGeneralMapper = new UserGeneralMapper();
            List<User> users = userGroupService.GetAllUsersFromAGroup(id);
            if (users == null || users.Count == 0)
                return NotFound("This group has no members");
            List<UserGeneralDTO> usersGeneralDTO = userGeneralMapper.Map(users);
            return Ok(usersGeneralDTO);
        }


        [HttpGet()]
        [Route("plan/{id}")]
        public IActionResult GetAllUsersFromAPlans(int id)
        {
            PlanService planService = new PlanService();
            if (planService.GetByID(id) == null)
                return NotFound("Invalid ID");
            PlanUserService planUserService = new PlanUserService();
            UserGeneralMapper userGeneralMapper = new UserGeneralMapper();
            List<User> users = planUserService.GetAllUsersFromAPlan(id);
            if (users == null || users.Count == 0)
                return NotFound("This plan has no members");
            List<UserGeneralDTO> usersGeneralDTO = userGeneralMapper.Map(users);
            return Ok(usersGeneralDTO);
        }

        [HttpDelete("{userId}/group/{groupId}")]
        public IActionResult DeleteUserFromGroup(int userId, int groupId)
        {
            UserService userService = new UserService();
            if (userService.GetByID(userId) == null)
                return NotFound("Invalid user id");
            GroupService groupService = new GroupService();
            if (groupService.GetByID(groupId) == null)
                return NotFound("Invalid group id");
            UserGroupService userGroupService = new UserGroupService();
            try
            {
                userGroupService.DeleteUserGroup(userId, groupId);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("{userId}/group/{groupId}")]
        public IActionResult PutUserGroup(int userId, int groupId, [FromBody] UserGroup userGroup)
        {
            UserService userService = new UserService();
            if (userService.GetByID(userId) == null)
                return NotFound("Invalid user id");
            GroupService groupService = new GroupService();
            if (groupService.GetByID(groupId) == null)
                return NotFound("Invalid group id");
            UserGroupService userGroupService = new UserGroupService();
            try
            {
                userGroup.UserID = userId;
                userGroup.GroupID = groupId;
                userGroup = userGroupService.UpdateUserGroup(userGroup);
                return Ok(userGroup);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("group/{groupId}/search")]
        public IActionResult GetAllUsersFromGroupPage(int groupId, int userId, string q, int page, int size)
        {
            UserService userService = new UserService();
            FriendshipService friendshipService = new FriendshipService();
            UserGeneralMapper userGeneralMapper = new UserGeneralMapper();
            List<User> source = userService.SearchForUsersOfGroup(userId, groupId, q);
            if (source == null || source.Count == 0)
            {
                return NotFound("There are no users!");
            }
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

            foreach (var u in usersDTO)
            {
                u.RelationshipStatus = friendshipService.GetUserRelation(u.ID, userId);
            }

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