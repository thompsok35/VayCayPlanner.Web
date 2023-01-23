using System.ComponentModel.DataAnnotations;

namespace VayCayPlanner.Data.Models
{
    public class TravelerDestination
    { 
        public int Id { get; set; }

        [MaxLength(256)]
        public string? FullName { get; set; }
        [MaxLength(256)]
        public string? EmailAddress { get; set; }

        public int DestinationId { get; set; }
    }
}
