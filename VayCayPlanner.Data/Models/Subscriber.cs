using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace VayCayPlanner.Data.Models
{
    public class Subscriber : IdentityUser
    {
        [MaxLength(256)]
        public string? FirstName { get; set; }
        [MaxLength(256)]
        public string? LastName { get; set; }
        [MaxLength(256)]
        public string? FullName { get; set; }
        [MaxLength(256)]
        public string? TravelerEmail { get; set; }

        [MaxLength(128)]
        public string? Mobile_Number { get; set; }
        public DateTime? DateJoined { get; set; }
        //public List<TravelGroup>? TravelGroups { get; set; }

        [MaxLength(450)]
        public string? DefaultTravelGroupKey { get; set; }
    }
}
