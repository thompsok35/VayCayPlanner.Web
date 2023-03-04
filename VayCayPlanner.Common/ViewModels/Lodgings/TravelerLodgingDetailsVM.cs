using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VayCayPlanner.Common.ViewModels.Lodgings
{
    public class TravelerLodgingDetailsVM
    {
        [Display(Name = "Transport Type")]
        public string? Name { get; set; }
        public int DestinationId { get; set; }

        [Display(Name = "Lodging Type")]
        public string LodgingType { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public int? MaxOccupancy { get; set; }
        public int? Nights { get; set; }

        [Display(Name = "Cost Per Night")]
        public decimal CostPerNight { get; set; }

        [Display(Name = "Total Cost")]
        public decimal TotalCost { get; set; }

        [Display(Name = "Link")]
        public string? WebLink { get; set; }
    }
}
