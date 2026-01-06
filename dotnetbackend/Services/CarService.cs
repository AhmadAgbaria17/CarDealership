using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetbackend.Dtos.Car;
using dotnetbackend.Helpers;
using dotnetbackend.IRepository;
using dotnetbackend.IServices;
using dotnetbackend.Mappers;
using dotnetbackend.models;





namespace dotnetbackend.Services
{
  public class CarService : ICarService
  {
  
    private readonly ICarRepository _carRepository;
    
    public CarService(ICarRepository carRepository)
    {
    
      _carRepository = carRepository;
    }



    public async Task<CarDto?> AddCarAsync(int CarDealerShipId, CreateCarRequest carDto)
    {
      var car = carDto.ToCarFromCreateDto(CarDealerShipId);
      await _carRepository.AddCarAsync(car);
      return car.ToCarDto();
    }

    public async Task<bool> DeleteCarAsync(int id)
    {

      return await _carRepository.DeleteCarAsync(id);
    }

    public async Task<List<CarDto>> GetAllCarsAsync(CQueryObject queryObject)
    {
      var cars = (await _carRepository.GetAllCarsAsync(queryObject))
        .Select(car => car.ToCarDto())
        .ToList();
      return cars;
    }

    public async Task<CarDto?> GetCarByIdAsync(int id)
    {
      return (await _carRepository.GetCarByIdAsync(id))?.ToCarDto();
    }

    public async Task<List<CarDto>> GetCarsByDealerShipIdAsync(int dealerShipId)
    {
      var cars = (await _carRepository.GetCarsByCarDealerShipIdAsync(dealerShipId))
        .Select(car => car.ToCarDto())
        .ToList();
      return cars;
      
    }

    public async Task<CarDto?> UpdateCarAsync(int id, UpdateCarRequest carDto)
    {
      var updatedCar = await _carRepository.UpdateCarAsync(id, carDto);
  
      return updatedCar?.ToCarDto();
    }
  }
}