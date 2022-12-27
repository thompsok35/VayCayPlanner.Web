using System.ComponentModel.DataAnnotations;

namespace VayCayPlanner.Web.Data.Models
{
    public class Traveler
    {
        public int Id { get; set; }
        [MaxLength(256)]
        public string? FullName { get; set; }
        [MaxLength(256)]
        public string? EmailAdress { get; set; }
    }
}
