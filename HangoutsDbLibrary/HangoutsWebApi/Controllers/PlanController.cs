using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HangoutsBusinessLibrary.Services;
using HangoutsWebApi.Mappings;
using HangoutsDbLibrary.Model;
using HangoutsWebApi.DTOModels;
using HangoutsWebApi.Services;

namespace HangoutsWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Plan")]
    public class PlanController : Controller
    {
        [HttpGet]
        public IActionResult GetAllPlans()
        {
            PlanService planService = new PlanService();
            PlanMapper planMapper = new PlanMapper();
            List<Plan> plans = planService.GetAllPlans();
            if (plans == null || plans.Count == 0)
            {
                return NotFound("There are no plans!");
            }
            List<PlanDTO> plansDTO = planMapper.Map(plans);
            return Ok(plansDTO);
        }

        [HttpGet("{id}")]
        public IActionResult GetPlanByID(int id)
        {
            PlanService planService = new PlanService();
            Plan plan = planService.GetByID(id);
            PlanMapper planMapper = new PlanMapper();
            PlanDTO planDTO = planMapper.Map(plan);
            if (plan == null)
            {
                return NotFound("There is no plan with such an ID");
            }
            return Ok(planDTO);
        }

        [HttpPost]
        public IActionResult Post([FromBody] PlanDTO planDTO)
        {
            if (ModelState.IsValid)
            {
                PlanService planService = new PlanService();
                PlanMapper planMapper = new PlanMapper();
                Plan plan = planMapper.Map(planDTO);
                var res = planService.AddPlan(plan);
                if (res.Equals("Ok"))
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(res);
                }
            }
            else
            {
                return BadRequest("The model is not valid!");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            PlanService planService = new PlanService();
            var res = planService.DeletePlan(id);

            if (res.Equals("Ok"))
                return Ok();
            else
                return BadRequest(res);
        }

        // Get all plans from a group
        [HttpGet("group/{id}")]
        public IActionResult GetAllPlansFromGroup(int id)
        {
            PlanService planService = new PlanService();
            GroupService groupService = new GroupService();
            PlanMapper planMapper = new PlanMapper();
            if (groupService.GetByID(id) == null)
            {
                return NotFound("Invalid group ID");
            }
            List<Plan> plans = planService.GetAllPlansFromGroup(id);
            if (plans == null || plans.Count == 0)
            {
                return NotFound("There are no plans!");
            }
            List<PlanDTO> plansDTO = planMapper.Map(plans);
            return Ok(plansDTO);
        }

        [HttpPost("user")]
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

        // Add a new plan to group
        [HttpPost("group")]
        public IActionResult PostToGroup([FromBody] PlanDTO planDTO)
        {
            if (ModelState.IsValid)
            {
                GroupService groupService = new GroupService();
                PlanService planService = new PlanService();
                PlanMapper planMapper = new PlanMapper();
                if (groupService.GetByID(planDTO.ID) == null)
                {
                    return BadRequest("Invalid group ID");
                }
                Plan plan = planMapper.Map(planDTO);
                var res = planService.AddPlan(plan);
                if (res.Equals("Ok"))
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(res);
                }
            }
            else
            {
                return BadRequest("The model is not valid!");
            }
        }

        // Get all plans of an user
        [HttpGet("user/{id}")]
        public IActionResult GetAllPlansOfUser(int id)
        {
            PlanService planService = new PlanService();
            UserService userService = new UserService();
            PlanMapper planMapper = new PlanMapper();
            if (userService.GetByID(id) == null)
            {
                return NotFound("Invalid user ID"); 
            }
            List<Plan> plans = planService.GetAllPlansOfUser(id);
            if (plans == null || plans.Count == 0)
            {
                return NotFound("There are no plans!");
            }
            List<PlanDTO> plansDTO = planMapper.Map(plans);
            return Ok(plansDTO);
        }
    }
}