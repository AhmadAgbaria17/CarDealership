using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetbackend.models;
using Microsoft.EntityFrameworkCore;

namespace dotnetbackend.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions dbContextOptions)
    : base(dbContextOptions)
    {

    }
    public DbSet<Person> Person { get; set; }

    public DbSet<Car> Car { get; set; }
        
    public DbSet<CarDealerShips> CarDealerShips { get; set; }


    }
}