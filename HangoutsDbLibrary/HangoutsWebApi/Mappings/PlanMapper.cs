using AutoMapper;
using HangoutsBusinessLibrary.Services;
using HangoutsDbLibrary.Model;
using HangoutsWebApi.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangoutsWebApi.Mappings
{
    public class PlanMapper
    {
        public PlanDTO Map(Plan plan)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Plan, PlanDTO>()
                .ForMember(dst => dst.ActivityID, 
                    op => op.MapFrom(src => src.Activity.ID));
            });
            IMapper mapper = config.CreateMapper();
            PlanDTO planDTO = mapper.Map<Plan, PlanDTO>(plan);
            ActivityService activityService = new ActivityService();
            Activity activity = new Activity();
            if (planDTO != null)
                activity = activityService.GetByID(planDTO.ActivityID);
            if (activity != null && planDTO != null)
                planDTO.Activity = activity.Description;
            //planDTO.ActivityID = plan.Activity.ID;
            return planDTO;
        }

        public List<PlanDTO> Map(List<Plan> plans)
        {
            List<PlanDTO> plansDTO = new List<PlanDTO>();
            foreach (Plan plan in plans)
            {
                plansDTO.Add(Map(plan));
            }
            return plansDTO;
        }

        public Plan Map(PlanDTO planDTO)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PlanDTO, Plan>()
                .ForMember(dst => dst.Address, op => op.Ignore())
                .ForMember(dst => dst.Activity, op => op.Ignore())
                .ForMember(dst => dst.Chat, op => op.Ignore());
            });
            IMapper mapper = config.CreateMapper();
            Plan plan = mapper.Map<PlanDTO, Plan>(planDTO);

            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AddressDTO, Address>()
                .ForMember(dst => dst.Users, op => op.Ignore());
            });
            mapper = config.CreateMapper();
            Address address = mapper.Map<AddressDTO, Address>(planDTO.Address);

            plan.Address = address;
            return plan;
        }

        public List<Plan> Map(List<PlanDTO> plansDTO)
        {
            List<Plan> plans = new List<Plan>();
            foreach (var planDTO in plansDTO)
            {
                plans.Add(Map(planDTO));
            }
            return plans;
        }
    }
}
