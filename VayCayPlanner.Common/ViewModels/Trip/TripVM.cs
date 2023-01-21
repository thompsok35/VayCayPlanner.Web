using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VayCayPlanner.Common.ViewModels.Trip
{
    public class TripVM
    {
        public int Id { get; set; }
        public int TravelGroupId { get; set; }

        [Display(Name = "Trip Name")]
        public string TripName { get; set; }

        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Created by")]
        public string OwnerId { get; set; }

    }
}
