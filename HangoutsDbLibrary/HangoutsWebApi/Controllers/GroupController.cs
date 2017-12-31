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

        [HttpGet("{groupId}")]
        public IActionResult GetGroupByID(int groupId, int userId)
        {
            GroupService groupService = new GroupService();
            UserService userService = new UserService();
            UserGroupService userGroupService = new UserGroupService();
            GroupMapper groupMapper = new GroupMapper();
            Group group = groupService.GetByID(groupId);
            if (group == null)
            {
                return NotFound("There is not group with such an ID");
            }
            GroupDTO groupDTO = groupMapper.Map(group);
            User user = userService.GetByID(userId);
            if(user == null)
                return Ok(groupDTO);
            groupDTO.Status = userGroupService.GetUserGroupStatus(groupId, userId);
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
                    GroupDTO groupDTOResponse = groupMapper.Map(group);
                    return Ok(groupDTOResponse);
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

        [HttpGet("search")]
        public IActionResult GetAllGroupsSearchPage(int id, string q, int page, int size)
        {
            GroupService groupService = new GroupService();
            GroupMapper groupMapper = new GroupMapper();
            List<Group> source;
            try
            {
                source = groupService.GetAllGroups(q);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            if (source == null || source.Count == 0)
            {
                return NotFound("We couldn't find anything for " + q);
            }

            int count = source.Count;
            int totalPages = (int)Math.Ceiling(count / (double)size);

            if (page > totalPages)
                return BadRequest("Page number out of range");

            List<Group> groups;
            if ((page - 1) * size + size < count)
                groups = source.GetRange((page - 1) * size, size);
            else
                groups = source.GetRange((page - 1) * size, count - (page - 1) * size);
            var previousPage = page > 1 ? "Yes" : "No";
            var nextPage = page < totalPages ? "Yes" : "No";
            List<GroupDTO> groupsDTO = groupMapper.Map(groups);
            UserGroupService userGroupService = new UserGroupService();
            foreach (var g in groupsDTO)
                g.Status = userGroupService.GetUserGroupStatus(g.ID, id);

            var response = new
            {
                totalCount = count,
                pageSize = size,
                currentPage = page,
                totalPages = totalPages,
                previousPage,
                nextPage,
                groups = groupsDTO
            };

            return Ok(response);
        }

        [HttpGet("{id}/my/{status}/search")]
        public IActionResult GetMyGroupsSearchPage(int id, string status, string q, int page, int size)
        {
            GroupService groupService = new GroupService();
            GroupMapper groupMapper = new GroupMapper();
            List<Group> source;
            try
            {
                source = groupService.GetMyGroups(id, status, q);
            }
            catch (Exception e) {
                return BadRequest(e.Message);
            }
            if (source == null || source.Count == 0)
            {
                return NotFound("We couldn't find anything for " + q);
            }
            int count = source.Count;
            int totalPages = (int)Math.Ceiling(count / (double)size);

            if (page > totalPages)
                return BadRequest("Page number out of range");

            List<Group> groups;
            if ((page - 1) * size + size < count)
                groups = source.GetRange((page - 1) * size, size);
            else
                groups = source.GetRange((page - 1) * size, count - (page - 1) * size);
            var previousPage = page > 1 ? "Yes" : "No";
            var nextPage = page < totalPages ? "Yes" : "No";
            List<GroupDTO> groupsDTO = groupMapper.Map(groups);

            var response = new
            {
                totalCount = count,
                pageSize = size,
                currentPage = page,
                totalPages = totalPages,
                previousPage,
                nextPage,
                groups = groupsDTO
            };

            return Ok(response);
        }

        [HttpGet("{id}/administrated/search")]
        public IActionResult GetGroupsAdministratedSearchPage(int id, string q, int page, int size)
        {
            GroupService groupService = new GroupService();
            GroupMapper groupMapper = new GroupMapper();
            List<Group> source = groupService.GetGroupsAdministrated(id, q);
            if (source == null || source.Count == 0)
            {
                return NotFound("We couldn't find anything for " + q);
            }
            int count = source.Count;
            int totalPages = (int)Math.Ceiling(count / (double)size);

            if (page > totalPages)
                return BadRequest("Page number out of range");

            List<Group> groups;
            if ((page - 1) * size + size < count)
                groups = source.GetRange((page - 1) * size, size);
            else
                groups = source.GetRange((page - 1) * size, count - (page - 1) * size);
            var previousPage = page > 1 ? "Yes" : "No";
            var nextPage = page < totalPages ? "Yes" : "No";
            List<GroupDTO> groupsDTO = groupMapper.Map(groups);

            var response = new
            {
                totalCount = count,
                pageSize = size,
                currentPage = page,
                totalPages = totalPages,
                previousPage,
                nextPage,
                groups = groupsDTO
            };

            return Ok(response);
        }

        [Route("~/api/user/availabletoinvite/group")]
        public IActionResult GetUsersAvailableToInvite(int id, int page, int size)
        {
            GroupService groupService = new GroupService();
            UserGeneralMapper userGeneralMapper = new UserGeneralMapper();
            List<User> source = new List<User>();
            try {
                source = groupService.GetFriendsAvailableToInviteToGroup(id);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
    }

            if (source == null || source.Count == 0)
            {
                return NotFound("There are no friends to invite");
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

        [Route("~/api/user/askedtojoin/group")]
        public IActionResult GetUsersWhoAskedToJoinGroup(int id, int page, int size)
        {
            GroupService groupService = new GroupService();
            UserGeneralMapper userGeneralMapper = new UserGeneralMapper();
            List<User> source = new List<User>();
            try
            {
                source = groupService.GetUsersWhoAskedToJoinGroup(id);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            if (source == null || source.Count == 0)
            {
                return NotFound("There are no friends to invite");
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

        [Route("~/api/user/invited/group")]
        public IActionResult GetInvitedUsers(int id, int page, int size)
        {
            GroupService groupService = new GroupService();
            UserGeneralMapper userGeneralMapper = new UserGeneralMapper();
            List<User> source = new List<User>();
            try
            {
                source = groupService.GetInvitedUsers(id);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            if (source == null || source.Count == 0)
            {
                return NotFound("There are no invited friends");
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
    }
}