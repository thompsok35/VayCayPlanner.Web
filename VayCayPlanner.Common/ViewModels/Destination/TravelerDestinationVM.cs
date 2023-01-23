using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VayCayPlanner.Common.ViewModels.Destination
{
    public class TravelerDestinationVM
    {
        public int Id { get; set; }
       
        public string? FullName { get; set; }
       
        public string? EmailAddress { get; set; }
        public int DestinationId { get; set; }
        public int TripId { get; set; }

    }
}
