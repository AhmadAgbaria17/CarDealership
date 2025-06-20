using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetbackend.Dtos.Car;
using dotnetbackend.models;

namespace dotnetbackend.IRepository
{
  public interface ICarRepository
  {
    Task<List<Car>> GetAllCarsAsync();
    Task<Car> GetCarByIdAsync(int id);
    Task<List<Car>> GetCarsByCarDealerShipIdAsync(int carDealerShipId);
    Task<Car> AddCarAsync(Car car);
    Task<Car> UpdateCarAsync(int id, UpdateCarRequest car);
    Task<bool> DeleteCarAsync(int id);
        
    }
}