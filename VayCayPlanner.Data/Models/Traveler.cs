using System.ComponentModel.DataAnnotations;

namespace VayCayPlanner.Data.Models
{
    public class Traveler : BaseEntity
    {        
        [MaxLength(256)]
        public string? FullName { get; set; }
        [MaxLength(256)]
        public string? EmailAddress { get; set; }
    }
}
