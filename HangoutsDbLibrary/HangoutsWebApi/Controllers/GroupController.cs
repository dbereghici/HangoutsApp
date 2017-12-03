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
            if(groups == null || groups.Count == 0)
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
            if(group == null)
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
                if(groupDTO.Name == null || groupDTO.Name.Length < 3 || groupDTO.Name.Length > 40)
                {
                    return BadRequest("The group name must contain from 3 to 40 characters!");
                }
                GroupService groupService = new GroupService();
                GroupMapper groupMapper = new GroupMapper();
                Group group = groupMapper.Map(groupDTO);
                group.Admin = null;
                var res = groupService.AddGroup(group);
                if (res.Equals("Ok"))
                    return Ok();
                else
                    return BadRequest(res);
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
            if (groupDTO.Name == null || groupDTO.Name.Length < 3 || groupDTO.Name.Length > 40)
            {
                return BadRequest("The group name must contain from 3 to 40 characters!");
            }
            Group group = groupMapper.Map(groupDTO);
            var existGroup = groupService.GetByID(id);
            string res;
            if (existGroup == null) {
                group.AdminID = id;
                res = groupService.AddGroup(group);
            }
            else
                res = groupService.UpdateGroup(group, id);
            if (res.Equals("Ok"))
                return Ok();
            else
                return BadRequest(res);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            GroupService groupService = new GroupService();
            var res = groupService.DeleteGroup(id);

            if (res.Equals("Ok"))
                return Ok();
            else
                return BadRequest(res);
        }

        [HttpPost("user")]
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
            string res = userGroupService.DeleteUserGroup(userId, groupId);
            if (res.Equals("Ok"))
                return Ok();
            else
                return NotFound(res);
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