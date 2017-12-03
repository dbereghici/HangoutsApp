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
            if (ModelState.IsValid)
            {
                if (activityDTO.Description == null || activityDTO.Description.Length < 3 || activityDTO.Description.Length > 40)
                {
                    return BadRequest("The activity description must contain from 3 to 40 characters!");
                }
                ActivityService activityService = new ActivityService();
                ActivityMapper activityMapper = new ActivityMapper();
                Activity activity = activityMapper.Map(activityDTO);
                var res = activityService.AddActivity(activity);
                if (res != null)
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
        public IActionResult Put(int id, [FromBody] ActivityDTO activityDTO)
        {
            ActivityService activityService = new ActivityService();
            ActivityMapper activityMapper = new ActivityMapper();
            if (!ModelState.IsValid)
            {
                return BadRequest("The model is not valid!");
            }
            if (activityDTO.Description == null || activityDTO.Description.Length < 3 || activityDTO.Description.Length > 40)
            {
                return BadRequest("The activity description must contain from 3 to 40 characters!");
            }
            Activity activity = activityMapper.Map(activityDTO);
            var existActivity = activityService.GetByID(id);
            Activity res = null;
            if (existActivity == null)
                res = activityService.AddActivity(activity);
            else
                res = activityService.UpdateActivity(activity, id);
            if (res != null)
                return Ok();
            else
                return BadRequest(res);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            ActivityService activityService = new ActivityService();
            var res = activityService.DeleteActivity(id);

            if (res != null)
                return Ok();
            else
                return BadRequest(res);
        }
    }
}
