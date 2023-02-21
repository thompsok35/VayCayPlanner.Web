using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VayCayPlanner.Common.ViewModels.Transports
{
    public class AddTransportSelectOptionsVM
    {
        public int? Id { get; set; }
        public int? TripId { get; set; }
        public int? TravelGroupId { get; set; }
        public int? DestinationId { get; set; }

        [Display(Name = "Trip Name")]
        public string? TripName { get; set; }

        [Display(Name = "From")]
        public string? DepartFrom { get; set; }

        [Display(Name = "To")]
        public string? ArriveIn { get; set; }

        public SelectList? TransportType { get; set; }

        public SelectList? Destination { get; set; }

        public List<TravelersVM>? DestinationTravelers { get; set; }
    }
}
