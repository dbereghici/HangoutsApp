using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HangoutsDbLibrary.Model;
using HangoutsDbLibrary.Data;
using HangoutsDbLibrary.Repository;
using Microsoft.EntityFrameworkCore;
using HangoutsWebApi.Services;
using HangoutsWebApi.DTOModels;
using AutoMapper;

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
            List<User> users = userService.getAllUsers();
            List<UserDTO> usersDTO = new List<UserDTO>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>();
            });
            IMapper mapper = config.CreateMapper();
            foreach (var user in users)
            {
                usersDTO.Add(mapper.Map<User, UserDTO>(user));
            }
            //List<UserDTO> usersDTO = userService.getAllUsers();
            return Ok(usersDTO);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserByID(int id)
        {
            UserService userService = new UserService();
            User user = userService.getByID(id);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>();
            });
            IMapper mapper = config.CreateMapper();
            UserDTO userDTO = mapper.Map<User, UserDTO>(user);
            if(user == null)
            {
                return NotFound("User with this id does not exist!");
            }
            return Ok(userDTO);
        }

        [HttpGet()]
        [Route("group/{id}")]
        public IActionResult GetAllUsersFromAGroup(int id)
        {
            UserService userService = new UserService();
            List<UserDTO> usersDTO = new List<UserDTO>();
            List<User> users = userService.getAllUsersFromAGroup(id);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>();
            });
            foreach(var user in users)
            {
                IMapper mapper = config.CreateMapper();
                usersDTO.Add(mapper.Map<User, UserDTO>(user));
            }
            return Ok(usersDTO);
        }
    }
    
}