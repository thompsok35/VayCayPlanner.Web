using System.ComponentModel.DataAnnotations;

namespace VayCayPlanner.Web.Data.Models
{
    public class TravelGroup 
    {
        public int Id { get; set; }
        public string? GroupName { get; set; }
        public List<Traveler>? TravelerIds { get; set; }
        
        //UUID used to verify a new member 
        //This UUID will be generated when the group is first created
        [MaxLength(450)]
        public string? InvitationKey { get; set; }

    }
}
