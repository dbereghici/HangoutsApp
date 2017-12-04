using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HangoutsDbLibrary.Model;
using HangoutsWebApi.Services;
using HangoutsWebApi.DTOModels;
using AutoMapper;
using HangoutsWebApi.Mappings;
using System.ComponentModel.DataAnnotations;
using HangoutsBusinessLibrary.Services;

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

        public string ValidUserData(UserDTO userDTO)
        {
            if (userDTO.Address == null || userDTO.BirthDate == null || userDTO.Email == null || userDTO.FirstName == null || 
                userDTO.LastName == null || userDTO.Username == null || userDTO.Password == null)
                return "All fields must be completed!";
            var emailAttr = new EmailAddressAttribute();
            if (!new EmailAddressAttribute().IsValid(userDTO.Email))
                return "The email input is not valid!";
            if (userDTO.Email.Length > 40 || userDTO.FirstName.Length > 40 ||
                 userDTO.LastName.Length > 40 || userDTO.Password.Length > 40 || userDTO.Username.Length > 40)
                return "The maximum length for a field is 40 characters!";
            if (userDTO.Password.Length < 5 || userDTO.Username.Length < 5)
                return "The username and password must contain at least 5 characters!";
            return "Ok";
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserDTO userDTO)
        {
            if (ModelState.IsValid && ValidUserData(userDTO).Equals("Ok"))
            {
                UserService userService = new UserService();
                UserMapper userMapper = new UserMapper();
                User user = userMapper.Map(userDTO);
                var res = userService.AddUser(user);
                if (res.Equals("Ok"))
                    return Ok();
                else
                    return BadRequest(res);
            }
            else
            {
                if (!ModelState.IsValid)
                    return BadRequest("The model is not valid!");
                else
                    return BadRequest(ValidUserData(userDTO));
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
            var validUserDataRes = ValidUserData(userDTO);
            if (validUserDataRes.Equals("Ok"))
            {
                User user = userMapper.Map(userDTO);
                var existUser = userService.GetByID(id);
                string res;
                if (existUser == null)
                    res = userService.AddUser(user);
                else 
                    res = userService.UpdateUser(id, user); 
                if (res.Equals("Ok"))
                    return Ok();
                else
                    return BadRequest(res);
            }
            else
            {
                return BadRequest(validUserDataRes);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            UserService userService = new UserService();
            var res = userService.DeleteUser(id);

            if (res.Equals("Ok"))
                return Ok();
            else
                return BadRequest(res);
        }

        [HttpPost("group")]
        public IActionResult AddUserToGroup([FromBody] UserGroupDTO userGroupDTO)
        {
            UserGroupService userGroupService = new UserGroupService();
            UserGroupMapper userGroupMapper = new UserGroupMapper();
            UserGroup userGroup = userGroupMapper.Map(userGroupDTO);
            string res = userGroupService.AddUserGroup(userGroup);
            if (res.Equals("Ok"))
                return Ok();
            else
                return BadRequest(res);
        }

        [HttpPost("plan")]
        public IActionResult AddUserToPlan([FromBody] PlanUserDTO planUserDTO)
        {
            PlanUserService planUserService = new PlanUserService();
            PlanUser planUser = new PlanUser { PlanID = planUserDTO.PlanID, UserID = planUserDTO.UserID };
            string res = planUserService.AddPlanUser(planUser);
            if (res.Equals("Ok"))
                return Ok();
            else
                return BadRequest(res);
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
                return NotFound("User" + id + " has no friends");
            List<UserGeneralDTO> friendsDTO = userGeneralMapper.Map(friends);
            return Ok(friendsDTO);
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
            string res = userGroupService.DeleteUserGroup(userId, groupId);
            if (res.Equals("Ok"))
                return Ok();
            else
                return NotFound(res);
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
            var existUserGroup = userGroupService.GetByID(groupId, userId);
            string res;
            if (existUserGroup == null)
            {
                userGroup.UserID = userId;
                userGroup.GroupID = groupId;
                res = res = userGroupService.AddUserGroup(userGroup);
            }
            else
            {
                userGroup.UserID = userId;
                userGroup.GroupID = groupId;
                res = userGroupService.UpdateUserGroup(userGroup);
            }
            if (res.Equals("Ok"))
                return Ok();
            else
                return NotFound(res);
        }
    }
}