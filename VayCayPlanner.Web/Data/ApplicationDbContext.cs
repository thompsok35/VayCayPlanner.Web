using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VayCayPlanner.Web.Data.Models;

namespace VayCayPlanner.Web.Data
{

    public class ApplicationDbContext : IdentityDbContext<Subscriber>
    //TODO: this inherits from Identity as type Subscriber (the extended class we created)
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Traveler> Travelers { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<TravelGroup> TravelGroups { get; set; }
    }

}