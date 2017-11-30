using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HangoutsDbLibrary.Model;
using HangoutsWebApi.Services;
using HangoutsWebApi.DTOModels;
using AutoMapper;
using HangoutsWebApi.Mappings;
using System.ComponentModel.DataAnnotations;

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
                return NotFound("User with this id does not exist!");
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
            return "OK";
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserDTO userDTO)
        {
            if (ModelState.IsValid && ValidUserData(userDTO).Equals("OK"))
            {
                UserService userService = new UserService();
                UserMapper userMapper = new UserMapper();
                User user = userMapper.Map(userDTO);
                userService.AddUser(user);
                return Ok();
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
            User user = userMapper.Map(userDTO);
            userService.UpdateUser(id, user);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            UserService userService = new UserService();
            userService.DeleteUser(id);
            return Ok();
        }

        [HttpGet()]
        [Route("group/{id}")]
        public IActionResult GetAllUsersFromAGroup(int id)
        {
            UserService userService = new UserService();
            UserGeneralMapper userGeneralMapper = new UserGeneralMapper();
            List<User> users = userService.GetAllUsersFromAGroup(id);
            List<UserGeneralDTO> usersGeneralDTO = userGeneralMapper.Map(users);
            return Ok(usersGeneralDTO);
        }
    }
    
}