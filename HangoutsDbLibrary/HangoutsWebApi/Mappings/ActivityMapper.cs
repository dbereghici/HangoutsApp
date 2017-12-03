using AutoMapper;
using HangoutsDbLibrary.Model;
using HangoutsWebApi.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangoutsWebApi.Mappings
{
    public class ActivityMapper
    {
        public ActivityDTO Map(Activity activity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Activity, ActivityDTO>()
                .ForMember(dst => dst.GroupDTO,
                    //op => op.MapFrom(src => src.Group));
                    op => op.Ignore());
            });
            IMapper mapper = config.CreateMapper();
            ActivityDTO activityDTO = mapper.Map<Activity, ActivityDTO>(activity);

            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Group, GroupDTO>()
                .ForMember(dst => dst.Admin,
                    op => op.MapFrom(src => src.Admin));
            });
            mapper = config.CreateMapper();
            activityDTO.GroupDTO = mapper.Map<Group, GroupDTO>(activity.Group);

            return activityDTO;
        }

        public List<ActivityDTO> Map(List<Activity> activities)
        {
            List<ActivityDTO> activitiesDTO = new List<ActivityDTO>();
            foreach (var activity in activities)
            {
                activitiesDTO.Add(Map(activity));
            }
            return activitiesDTO;
        }

        public Activity Map(ActivityDTO activityDTO)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ActivityDTO, Activity>();
            });
            IMapper mapper = config.CreateMapper();
            Activity activity = mapper.Map<ActivityDTO, Activity>(activityDTO);

            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GroupDTO, Group>()
                .ForMember(g => g.UserGroups,
                    op => op.Ignore());
            });
            mapper = config.CreateMapper();
            Group group = mapper.Map<GroupDTO, Group>(activityDTO.GroupDTO);
            activity.Group = group;

            return activity;
        }

        public List<Activity> Map(List<ActivityDTO> activitiesDTO)
        {
            List<Activity> activities = new List<Activity>();
            foreach (var activityDTO in activitiesDTO)
            {
                activities.Add(Map(activityDTO));
            }
            return activities;
        }

    }
}
