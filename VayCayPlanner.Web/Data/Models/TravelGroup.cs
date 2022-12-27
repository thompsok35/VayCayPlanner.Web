using System.ComponentModel.DataAnnotations;

namespace VayCayPlanner.Web.Data.Models
{
    public class TravelGroup 
    {
        public int Id { get; set; }
        public string? GroupName { get; set; }
        public List<string>? UserIds { get; set; }
        
        //UUID used to verify a new member 
        //This UUID will be generated when the group is first created
        [MaxLength(256)]
        public string? InvitationKey { get; set; }

    }
}
