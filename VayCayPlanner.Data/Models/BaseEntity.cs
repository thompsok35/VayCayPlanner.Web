namespace VayCayPlanner.Data.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public string? TravelGroupId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
