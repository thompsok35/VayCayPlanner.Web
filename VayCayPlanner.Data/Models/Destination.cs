using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VayCayPlanner.Data.Models
{
    public class Destination : BaseEntity
    {
        public int TripId { get; set; }

        [MaxLength(450)]
        public string City { get; set; }

        [MaxLength(450)]
        public string Country { get;  set; }
        
        public DateTime? ArrivalDate { get; set; }
        public DateTime? DepartureDate { get; set; }
    }
}
//https://code-maze.com/csharp-dateonly-timeonly/