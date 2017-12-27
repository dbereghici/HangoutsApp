using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HangoutsWebApi.DTOModels;
using HangoutsWebApi.Services;
using HangoutsWebApi.Mappings;
using HangoutsDbLibrary.Model;
using HangoutsBusinessLibrary.Services;

namespace HangoutsWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Authentication")]
    public class AuthenticationController : Controller
    {
        [HttpPost]
        public IActionResult Post([FromBody] UserLoginDTO userLoginDTO)
        {
            if (ModelState.IsValid)
            {
                AuthenticationService authenticationService = new AuthenticationService();
                UserMapper userMapper = new UserMapper();
                User user = new User
                {
                    Username = userLoginDTO.Username,
                    Email = userLoginDTO.Email,
                    Password = userLoginDTO.Password
                };
                try
                {
                    user = authenticationService.Authenticate(user);
                    UserDTO userDTO = userMapper.Map(user);
                    return Ok(userDTO);
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
    }
}