using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VayCayPlanner.Common.Traveler.ViewModels
{
    public class TravelerDeleteVM
    {
        public int Id { get; set; }

        public string GroupName { get; set; }

        public int TravelGroupId { get; set; }
        
        [Display(Name = "Traveler Name")]
        public string? FullName { get; set; }

        [Display(Name = "Email Address")]
        public string? EmailAddress { get; set; }
    }
}
