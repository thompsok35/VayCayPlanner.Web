using VayCayPlanner.Data.Models;

namespace VayCayPlanner.Web.Models
{
    public class DeleteTripVM
    {
        public Trip trip { get; set; }
        public List<Destination> destinations { get; set; }
        public List<TravelerDestination> travelerDestinations { get; set; }
        public TravelGroup travelGroup { get; set; }
        public List<Traveler> travelers { get; set; }
    }
}
