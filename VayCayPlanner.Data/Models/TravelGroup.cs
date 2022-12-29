using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VayCayPlanner.Data.Models
{
    public class TravelGroup 
    {

        public int Id { get; set; }

        [MaxLength(450)]
        public string GroupName { get; set; }

        public int TypeId { get; set; }

        //public List<Traveler>? Travelers { get; set; }

        [MaxLength(450)]
        public string OwnerId { get; set; }

        //UUID used to verify a new member 
        //This UUID will be generated when the group is first created
        [MaxLength(450)]
        public string? InvitationKey { get; set; }



    }
}
