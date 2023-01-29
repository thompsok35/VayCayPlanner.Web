using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public int? MaxOccupancy { get; set; }
        public int Nights { get; set; }
        //public decimal CleaningFees { get; set; }
        //public decimal OtherFees { get; set; }
        //public decimal TotalCost { get; set; }
        public string? WebLink { get; set; }
    }
}
