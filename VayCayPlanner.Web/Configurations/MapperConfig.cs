using AutoMapper;
using VayCayPlanner.Common.Traveler.ViewModels;
using VayCayPlanner.Common.ViewModels;
using VayCayPlanner.Data.Models;

namespace VayCayPlanner.Web.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Traveler, TravelersVM>().ReverseMap();
            CreateMap<TravelGroup, TravelGroupDetailVM>().ReverseMap();
            CreateMap<TravelGroup, TravelGroupEditVM>().ReverseMap();
            CreateMap<Traveler, TravelerEditVM>().ReverseMap();
            CreateMap<Traveler, TravelerDetailVM>().ReverseMap();
            //CreateMap<LeaveRequest, LeaveRequestCreateVM>().ReverseMap();
            //CreateMap<LeaveRequest, LeaveRequestVM>().ReverseMap();
        }
    }
}
