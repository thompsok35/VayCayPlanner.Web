using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VayCayPlanner.Common.ViewModels.Lodgings
{
    public class AddLodgingVM
    {
        public int Id { get; set; }

        [Display(Name = "Lodging Name")]
        public string? Name { get; set; }
        public int TripId { get; set; }

        [Display(Name = "Trip Name")]
        public string? TripName { get; set; }

        [Display(Name = "Destination Name")]
        public string? DestinationName { get; set; }

        public int DestinationId { get; set; }
        public int LodgingTypeId { get; set; }

        [Display(Name = "Lodging Types")]
        public SelectList? LodgingTypes { get; set; }

        //[Display(Name = "Lodging Type")]
        //public string LodgingType { get; set; }

        [Display(Name = "Check in Date")]
        public DateTime? CheckInDate { get; set; }

        [Display(Name = "Check out Date")]
        public DateTime? CheckOutDate { get; set; }

        [Display(Name = "Max Occupancy")]
        public int? MaxOccupancy { get; set; }
        public int? Nights { get; set; }

        [Display(Name = "Cost Per Night")]
        public decimal CostPerNight { get; set; }

        [Display(Name = "Total Cost")]
        public decimal TotalCost { get; set; }

        [Display(Name = "Booking Link")]
        public string? WebLink { get; set; }

    }
}
