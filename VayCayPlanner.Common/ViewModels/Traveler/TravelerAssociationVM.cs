using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VayCayPlanner.Common.ViewModels.Traveler
{
    public class TravelerAssociationVM
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? EmailAddress { get; set; }
        public int ActivityId { get; set; }
        public int DestinationId { get; set; }
        public int LodgingId { get; set; }
        public int TripId { get; set; }
    }
}
