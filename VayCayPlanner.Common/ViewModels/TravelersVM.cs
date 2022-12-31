using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VayCayPlanner.Common.ViewModels
{
    public class TravelersVM
    {
        [Display(Name = "Traveler Name")]
        public string? FullName { get; set; }

        [Display(Name = "Email Address")]
        public string? EmailAddress { get; set; }

        [Display(Name = "Date Added")]
        public DateTime? CreatedDate { get; set; }
    }
}
