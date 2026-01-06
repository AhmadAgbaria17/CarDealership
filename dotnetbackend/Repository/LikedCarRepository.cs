using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetbackend.Data;
using dotnetbackend.IRepository;
using dotnetbackend.models;
using Microsoft.EntityFrameworkCore;

namespace dotnetbackend.Repository
{
  public class LikedCarRepository : ILikedCarRepository
  {
  private readonly ApplicationDbContext _context;
    public LikedCarRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<List<Car>> GetPersonLikedCar(Person person)
    {
      return await _context.LikedCars.Where(lc => lc.PersonId == person.Id)
      .Select(car => new Car
      {
        Id = car.CarId,
        Company = car.Car.Company,
        ModelName = car.Car.ModelName,
        Year = car.Car.Year,
      }).ToListAsync();
    }

    public async Task<bool> LikeCarAsync(Person person, int carId)
    {
      var alreadyLiked = await _context.LikedCars
        .AnyAsync(lc => lc.PersonId == person.Id && lc.CarId == carId);
      if (alreadyLiked)
      {
        return false; // Car already liked
      }

      var likedCar = new LikedCar
      {
        PersonId = person.Id,
        CarId = carId
      };
      

      await _context.LikedCars.AddAsync(likedCar);
      await _context.SaveChangesAsync();
      return true; 


    }

    public async Task<bool> UnlikeCarAsync(Person person, int carId)
    {
      var likedCar = await  _context.LikedCars
        .FirstOrDefaultAsync(lc => lc.PersonId == person.Id && lc.CarId == carId);
      if (likedCar == null)
      {
        return false;
      }
      _context.LikedCars.Remove(likedCar);
      await _context.SaveChangesAsync();
      return true; 
  
    }
  }
}