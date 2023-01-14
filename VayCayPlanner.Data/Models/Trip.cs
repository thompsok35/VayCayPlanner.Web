using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VayCayPlanner.Data.Models
{
    public class Trip : BaseEntity
    {
        public string TripName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [ForeignKey("TravelGroupId")]
        public TravelGroup? Travelers { get; set; }

        [MaxLength(450)]
        public string OwnerId { get; set; }

    }
}
//https://code-maze.com/csharp-dateonly-timeonly/
