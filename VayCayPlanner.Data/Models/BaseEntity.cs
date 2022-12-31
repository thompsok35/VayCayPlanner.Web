namespace VayCayPlanner.Data.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public int? TravelGroupId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
