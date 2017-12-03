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
                if (res != null)
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
                User res;
                if (existUser == null)
                    res = userService.AddUser(user);
                else 
                    res = userService.UpdateUser(id, user); 
                if (res == null)
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

            if (res == null)
                return Ok();
            else
                return BadRequest(res);
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