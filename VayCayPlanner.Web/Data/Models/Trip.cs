namespace VayCayPlanner.Web.Data.Models
{
    public class Trip : BaseEntity
    {
        public string? TripName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Traveler>? Travelers { get; set; }

    }
}
