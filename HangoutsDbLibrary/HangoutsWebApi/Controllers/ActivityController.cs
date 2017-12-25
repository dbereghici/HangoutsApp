using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HangoutsWebApi.Services;
using HangoutsWebApi.Mappings;
using HangoutsDbLibrary.Model;
using HangoutsWebApi.DTOModels;
using HangoutsBusinessLibrary.Services;

namespace HangoutsWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Activity")]
    public class ActivityController : Controller
    {
        [HttpGet]
        public IActionResult GetAllActivities()
        {
            ActivityService activityService = new ActivityService();
            ActivityMapper activityMapper = new ActivityMapper();
            List<Activity> activities = activityService.GetAllActivities();
            if (activities == null || activities.Count == 0)
            {
                return NotFound("There are not activities");
            }
            List<ActivityDTO> activitiesDTO = activityMapper.Map(activities);
            return Ok(activitiesDTO);
        }

        [HttpGet("{id}")]
        public IActionResult GetActivityByID(int id)
        {
            ActivityService activityService = new ActivityService();
            ActivityMapper activityMapper = new ActivityMapper();
            Activity activity = activityService.GetByID(id);
            if (activity == null)
            {
                return NotFound("There is not any group with such an ID");
            }
            ActivityDTO activityDTO = activityMapper.Map(activity);
            return Ok(activityDTO);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ActivityDTO activityDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("The model is not valid!");
            }
            ActivityService activityService = new ActivityService();
            ActivityMapper activityMapper = new ActivityMapper();
            Activity activity = activityMapper.Map(activityDTO);
            try
            {
                activity = activityService.AddActivity(activity);
                return Ok(activity);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ActivityDTO activityDTO)
        {
            ActivityService activityService = new ActivityService();
            ActivityMapper activityMapper = new ActivityMapper();
            if (!ModelState.IsValid)
            {
                return BadRequest("The model is not valid!");
            }
            Activity activity = activityMapper.Map(activityDTO);
            var existActivity = activityService.GetByID(id);
            try
            {
                activity = activityService.UpdateActivity(activity, id);
                return Ok(activity);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            ActivityService activityService = new ActivityService();
            try
            {
                activityService.DeleteActivity(id);
                return Ok();
            } 
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
