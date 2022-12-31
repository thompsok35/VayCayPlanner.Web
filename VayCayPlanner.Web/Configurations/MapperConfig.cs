using AutoMapper;
using VayCayPlanner.Common.ViewModels;
using VayCayPlanner.Data.Models;

namespace VayCayPlanner.Web.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Traveler, TravelersVM>().ReverseMap();
            //CreateMap<Employee, EmployeeListVM>().ReverseMap();
            //CreateMap<Employee, EmployeeAllocationVM>().ReverseMap();
            //CreateMap<LeaveAllocation, LeaveAllocationVM>().ReverseMap();
            //CreateMap<LeaveAllocation, LeaveAllocationEditVM>().ReverseMap();
            //CreateMap<LeaveRequest, LeaveRequestCreateVM>().ReverseMap();
            //CreateMap<LeaveRequest, LeaveRequestVM>().ReverseMap();
        }
    }
}
