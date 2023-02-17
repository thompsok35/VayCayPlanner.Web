using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VayCayPlanner.Data.Models
{
    public class Lodging : BaseEntity
    {
        [MaxLength(512)]
        public string? Name { get; set; }
        public int DestinationId { get; set; }
        public string LodgingType { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public int? MaxOccupancy { get; set; }
        public int? Nights { get; set; }

        [Column(TypeName = "money")]
        public decimal CostPerNight { get; set; }
        [Column(TypeName = "money")]
        public decimal TotalCost { get; set; }
        public string? WebLink { get; set; }
    }
}
