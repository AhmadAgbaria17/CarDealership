using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetbackend.Dtos.Car;
using dotnetbackend.models;

namespace dotnetbackend.IServices
{
  public interface ICarService
  {
    Task<List<CarDto>> GetAllCarsAsync();
    Task<CarDto?> GetCarByIdAsync(int id);
    Task<CarDto?> AddCarAsync(int CarDealerShipId ,CreateCarRequest carDto);
    Task<CarDto?> UpdateCarAsync(int id , UpdateCarRequest carDto);
    Task<bool> DeleteCarAsync(int id);
    Task<List<CarDto>> GetCarsByDealerShipIdAsync(int dealerShipId);
        
    }
}