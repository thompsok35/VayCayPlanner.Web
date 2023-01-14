using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VayCayPlanner.Common.ViewModels.Trip
{
    public class CreateNewTripVM
    {
        public int TripId { get; set; }
        public int GroupId { get; set; }
        public string TripName { get; set; }
        public string DestinationName { get; set; }
        public DateOnly DestinationStartDate { get; set; }
        public DateOnly DestinationEndDate { get; set; }


    }
}
