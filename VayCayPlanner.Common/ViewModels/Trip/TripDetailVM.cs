using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VayCayPlanner.Common.ViewModels.Trip
{
    public class TripDetailVM
    {
        public int Id { get; set; }

        [Display(Name = "Trip Name")]
        public string TripName { get; set; }

        [Display(Name = "Start date")]
        public DateTime? StartDate { get; set; }
        [Display(Name = "End date")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Duration")]
        public int Duration { get; set; }

        [Display(Name = "Days until departure")]
        public int DaysUntilDeparture { get; set; }

        [Display(Name = "Destinations")]
        public int Destinations { get; set; }

        [Display(Name = "Travelers")]
        public int Travelers { get; set; }

        //[Display(Name = "Activities")]
        //public int Activities { get; set; }

        //[Display(Name = "Transports")]
        //public int Transports { get; set; }

        //[Display(Name = "Total Cost")]
        //public double TotalCost { get; set; }

        //[Display(Name = "Created By")]
        //public string OwnerName { get; set; }


    }
}
