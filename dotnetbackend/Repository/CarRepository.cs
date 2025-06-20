using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetbackend.Data;
using dotnetbackend.Dtos.Car;
using dotnetbackend.IRepository;
using dotnetbackend.models;
using Microsoft.EntityFrameworkCore;

namespace dotnetbackend.Repository
{
  public class CarRepository : ICarRepository
  {
    private readonly ApplicationDbContext _context;
    public CarRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<Car> AddCarAsync(Car car)
    {
      await _context.Car.AddAsync(car);
      await _context.SaveChangesAsync();
      return car;
    }

    public async Task<bool> DeleteCarAsync(int id)
    {
      var car = await _context.Car.FindAsync(id);
      if (car is null)
      {
        return false;
      }
      _context.Car.Remove(car);
      await _context.SaveChangesAsync();
      return true;  
    }

    public async Task<List<Car>> GetAllCarsAsync()
    {
      return await _context.Car.ToListAsync();
    }

    public async Task<Car> GetCarByIdAsync(int id)
    {
      return await _context.Car.FindAsync(id)
              ?? throw new KeyNotFoundException($"Car with id {id} not found.");
    }

    public async Task<List<Car>> GetCarsByCarDealerShipIdAsync(int carDealerShipId)
    {
      return await _context.Car
        .Where(car => car.CarDealerShipId == carDealerShipId)
        .ToListAsync();
    }

    public async Task<Car> UpdateCarAsync(int id, UpdateCarRequest car)
    {
      var existingCar = await _context.Car.FindAsync(id);
      if (existingCar is null)
      {
        throw new KeyNotFoundException($"Car with id {id} not found.");
      }
      existingCar.Company = car.Company;
      existingCar.ModelName = car.ModelName;
      existingCar.Year = car.Year;
      existingCar.Color = car.Color;
      existingCar.Image = car.Image;
      existingCar.Description = car.Description;
      existingCar.Price = car.Price;
      existingCar.Fuel = car.Fuel;
      existingCar.Transmission = car.Transmission;
      existingCar.Mileage = car.Mileage;
      existingCar.Engine = car.Engine;
      existingCar.HorsePower = car.HorsePower;
      existingCar.Type = car.Type;


      _context.Car.Update(existingCar);
      await _context.SaveChangesAsync();
      return existingCar;
    }
  }
}