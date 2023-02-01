using AutoMapper;
using VayCayPlanner.Common.Traveler.ViewModels;
using VayCayPlanner.Common.ViewModels;
using VayCayPlanner.Common.ViewModels.Destination;
using VayCayPlanner.Common.ViewModels.Transports;
using VayCayPlanner.Common.ViewModels.Trip;
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
            CreateMap<Trip, TripVM>().ReverseMap();
            CreateMap<Destination, DestinationVM>().ReverseMap();
            CreateMap<TravelerDestination, TravelerDestinationVM>().ReverseMap();
            CreateMap<Transport, TransportToFirstDestinationVM>().ReverseMap();
            //CreateMap<LeaveRequest, LeaveRequestVM>().ReverseMap();
            //CreateMap<LeaveRequest, LeaveRequestVM>().ReverseMap();
            //CreateMap<LeaveRequest, LeaveRequestVM>().ReverseMap();
        }
    }
}
