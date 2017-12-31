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
                try
                {
                    plan = planService.AddPlan(plan);
                    return Ok(plan);
                }
                catch (Exception e)
                {
                    return NotFound(e.Message);
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
            try
            {
                planService.DeletePlan(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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
            try
            {
                planUser = planUserService.AddPlanUser(planUser);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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
                try
                {
                    plan = planService.AddPlan(plan);
                    return Ok();
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

        [HttpPost("similar")]
        public IActionResult GetSimilarPlansPage([FromBody]PlanMatchInputDTO data, int page, int size)
        {
            PlanService planService = new PlanService();
            PlanMapper planMapper = new PlanMapper();
            PlanUserService planUserService = new PlanUserService();

            //DateTime startTimeDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            //DateTime endTimeDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            //startTimeDate = startTimeDate.AddSeconds(startTime).ToLocalTime();
            //endTimeDate = endTimeDate.AddSeconds(endTime).ToLocalTime();

            List<Plan> source = planService.GetSimilarPlans(data.GroupID, data.StartTime, data.EndTime, data.ActivityDescription);

            if (source == null || source.Count == 0)
            {
                return NotFound("There are no similar plans!");
            }
            int count = source.Count;
            int totalPages = (int)Math.Ceiling(count / (double)size);

            if (page > totalPages)
                return BadRequest("Page number out of range");

            List<Plan> plans;
            if((page - 1) * size + size < count)
                plans = source.GetRange((page - 1) * size, size);
            else
                plans = source.GetRange((page - 1) * size, count - (page - 1) * size);
            var previousPage = page > 1 ? "Yes" : "No";
            var nextPage = page < totalPages ? "Yes" : "No";
            List<PlanDTO> plansDTO = planMapper.Map(plans);
            foreach (var plan in plansDTO)
            {
                plan.Status = planUserService.getStatus(plan.ID, data.UserID);
            }

            var response = new
            {
                totalCount = count, 
                pageSize = size,
                currentPage = page,
                totalPages = totalPages,
                previousPage,
                nextPage,
                plans = plansDTO
            };
            return Ok(response);
        }

        [HttpGet("all")]
        public IActionResult GetAllPlansPage(int userId, int page, int size)
        {
            PlanService planService = new PlanService();
            PlanMapper planMapper = new PlanMapper();
            PlanUserService planUserService = new PlanUserService();
            //List<Plan> source = planService.GetSimilarPlans(groupId, startTime, endTime, activityDescription);
            List<Plan> source = planService.GetAllPlans();
            if (source == null || source.Count == 0)
            {
                return NotFound("There are no similar plans!");
            }
            int count = source.Count;
            int totalPages = (int)Math.Ceiling(count / (double)size);

            if (page > totalPages)
                return BadRequest("Page number out of range");

            List<Plan> plans;
            if ((page - 1) * size + size < count)
                plans = source.GetRange((page - 1) * size, size);
            else
                plans = source.GetRange((page - 1) * size, count - (page - 1) * size);
            var previousPage = page > 1 ? "Yes" : "No";
            var nextPage = page < totalPages ? "Yes" : "No";
            List<PlanDTO> plansDTO = planMapper.Map(plans);
            foreach (var plan in plansDTO)
            {
                plan.Status = planUserService.getStatus(plan.ID, userId);
            }

            var response = new
            {
                totalCount = count,
                pageSize = size,
                currentPage = page,
                totalPages = totalPages,
                previousPage,
                nextPage,
                plans = plansDTO
            };
            return Ok(response);
        }
    }
}