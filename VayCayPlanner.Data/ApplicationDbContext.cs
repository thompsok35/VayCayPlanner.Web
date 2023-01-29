using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VayCayPlanner.Data.Models;

namespace VayCayPlanner.Data
{

    public class ApplicationDbContext : IdentityDbContext<Subscriber>
    //TODO: this inherits from Identity as type Subscriber (the extended class we created)
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Traveler> Travelers { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<TravelGroup> TravelGroups { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<OnBoarding> OnBoardings { get; set; }
        public DbSet<OnBoardingStep> OnBoardingSteps { get; set; }
        public DbSet<NewTripTemplate> NewTripTemplates { get; set; }
        public DbSet<TravelerDestination> TravelerDestinations { get; set; }
        public DbSet<Transport> Transports { get; set; }
        public DbSet<Lodging> Lodgings { get; set; }
    }

}