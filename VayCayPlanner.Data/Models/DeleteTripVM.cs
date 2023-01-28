using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VayCayPlanner.Data.Models
{
    public class DeleteTripVM
    {
        public int Id { get; set; }
        public int? TravelGroupId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string TripName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string OwnerId { get; set; }

        public Trip trip { get; set; }

        public List<Destination> destinations { get; set; }
        public List<TravelerDestination> travelerDestinations { get; set; }
        public TravelGroup travelGroup { get; set; }
        public List<Traveler> travelers { get; set; }

    }
}
