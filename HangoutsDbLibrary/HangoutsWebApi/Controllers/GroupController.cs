using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HangoutsWebApi.Services;
using HangoutsWebApi.Mappings;
using HangoutsWebApi.DTOModels;
using HangoutsDbLibrary.Model;

namespace HangoutsWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Group")]
    public class GroupController : Controller
    {
        [HttpGet]
        public IActionResult GetAllGroups()
        {
            GroupService groupService = new GroupService();
            GroupMapper groupMapper = new GroupMapper();
            List<Group> groups = groupService.GetAllGroups();
            if (groups == null || groups.Count == 0)
            {
                return NotFound("There are not groups");
            }
            List<GroupDTO> groupsDTO = groupMapper.Map(groups);
            return Ok(groupsDTO);
        }

        [HttpGet("{id}")]
        public IActionResult GetGroupByID(int id)
        {
            GroupService groupService = new GroupService();
            GroupMapper groupMapper = new GroupMapper();
            Group group = groupService.GetByID(id);
            if (group == null)
            {
                return NotFound("There is not group with such an ID");
            }
            GroupDTO groupDTO = groupMapper.Map(group);
            return Ok(groupDTO);
        }

        [HttpPost]
        public IActionResult Post([FromBody] GroupDTO groupDTO)
        {
            if (ModelState.IsValid)
            {
                GroupService groupService = new GroupService();
                GroupMapper groupMapper = new GroupMapper();
                Group group = groupMapper.Map(groupDTO);
                group.Admin = null;
                try
                {
                    group = groupService.AddGroup(group);
                    return Ok(group);
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
        public IActionResult Put(int id, [FromBody] GroupDTO groupDTO)
        {
            GroupService groupService = new GroupService();
            GroupMapper groupMapper = new GroupMapper();
            if (!ModelState.IsValid)
            {
                return BadRequest("The model is not valid!");
            }
            Group group = groupMapper.Map(groupDTO);
            try
            {
                group = groupService.UpdateGroup(group, id);
                return Ok(group);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            GroupService groupService = new GroupService();
            try
            {
                groupService.DeleteGroup(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("user")]
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

        [HttpDelete("{groupId}/user/{userId}")]
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
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{groupId}/user/{userId}")]
        public IActionResult PutUserGroup(int userId, int groupId, [FromBody] UserGroup userGroup)
        {
            UserService userService = new UserService();
            if (userService.GetByID(userId) == null)
                return NotFound("Invalid user id");
            GroupService groupService = new GroupService();
            if (groupService.GetByID(groupId) == null)
                return NotFound("Invalid group id");
            UserGroupService userGroupService = new UserGroupService();

            userGroup.UserID = userId;
            userGroup.GroupID = groupId;
            try { 
                userGroup = userGroupService.UpdateUserGroup(userGroup);
                return Ok(userGroup);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("user/{id}")]
        public IActionResult GetAllGroupOfUser(int id)
        {
            UserService userService = new UserService();
            if (userService.GetByID(id) == null)
                return NotFound("Invalid ID");
            GroupService groupService = new GroupService();
            GroupMapper groupMapper = new GroupMapper();
            List<Group> groups = groupService.GetAllGroupOfUser(id);
            if (groups == null || groups.Count == 0)
                return NotFound("This user is not part of any group");
            List<GroupDTO> groupsDTO = groupMapper.Map(groups);
            return Ok(groupsDTO);
        }
    }
}