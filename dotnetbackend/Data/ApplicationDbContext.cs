using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetbackend.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dotnetbackend.Data
{
  public class ApplicationDbContext : IdentityDbContext<Person>
  {
    public ApplicationDbContext(DbContextOptions dbContextOptions)
    : base(dbContextOptions)
    {

    }

    public DbSet<Car> Car { get; set; }

    public DbSet<CarDealerShips> CarDealerShips { get; set; }

    public DbSet<LikedCar> LikedCars { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<LikedCar>(x => x.HasKey(
        p => new { p.PersonId, p.CarId }
      ));

      modelBuilder.Entity<LikedCar>()
        .HasOne(p => p.Person)
        .WithMany(p => p.LikedCar)
        .HasForeignKey(p => p.PersonId);    

      modelBuilder.Entity<LikedCar>()
        .HasOne(p => p.Car)
        .WithMany(c => c.LikedCars)
        .HasForeignKey(p => p.CarId);    

      List<IdentityRole> roles = new List<IdentityRole>
      {
      new IdentityRole
        {
            Id = "a1b2c3d4-e5f6-7890-abcd-1234567890ab",
            Name = "Admin",
            NormalizedName = "ADMIN"
        },
         new IdentityRole
      {
          Id = "c3d4e5f6-7890-1234-abcd-3456789012cd",
          Name = "Customer",
          NormalizedName = "CUSTOMER"
      },
        new IdentityRole
        {
            Id = "b2c3d4e5-f678-9012-abcd-2345678901bc",
            Name = "User",
            NormalizedName = "USER"
        }
      };

      modelBuilder.Entity<IdentityRole>().HasData(roles);


    }
  }
}