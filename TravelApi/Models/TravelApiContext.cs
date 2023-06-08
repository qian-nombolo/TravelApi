using Microsoft.EntityFrameworkCore;
using System;

namespace TravelApi.Models
{
  public class TravelApiContext : DbContext
  {
    public DbSet<Travel> Travels { get; set; }
    
    public TravelApiContext(DbContextOptions<TravelApiContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Travel>()
        .HasData(
          new Travel { TravelId = 1, Destination = "Angkor Wat", City = "Siem Reap", Country = "Cambodia", Review = "Spiritual", Rating = 7, Date = new DateTime (2009, 12, 1)},
          
          new Travel { TravelId = 2, Destination = "Great Wall", City = "Beijing", Country = "China", Review = "Great", Rating = 9, Date = new DateTime(2023, 7, 1)},
          
          new Travel { TravelId = 3, Destination = "Louvre Museum", City = "Paris", Country = "France", Review= "Crowded", Rating = 4, Date = new DateTime(2023, 6, 3)},
          
          new Travel { TravelId = 4, Destination = "Big Ben", City = "London", Country = "United Kingdom", Review = "Great", Rating = 9, Date = new DateTime (2023, 5, 6)},

          new Travel { TravelId = 5, Destination = "Summer Palace", City = "Beijing", Country = "China", Review = "Good", Rating = 7, Date = new DateTime(2024, 7, 1)},
          
          new Travel { TravelId = 6, Destination = "Temple of Heaven", City = "Beijing", Country = "China", Review = "Great", Rating = 8, Date = new DateTime(2022, 7, 1)}
          
      );
    }

  }
}