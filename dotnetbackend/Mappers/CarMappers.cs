using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetbackend.Dtos.Car;
using dotnetbackend.models;

namespace dotnetbackend.Mappers
{
  public static class CarMappers
  {
    public static CarDto ToCarDto(this Car car)
    {
      return new CarDto
      {
        Id = car.Id,
        Company = car.Company,
        ModelName = car.ModelName,
        Year = car.Year,
        Color = car.Color,
        Images = car.Images,
        Description = car.Description,
        Price = car.Price,
        Fuel = car.Fuel,
        Transmission = car.Transmission,
        Mileage = car.Mileage,
        Engine = car.Engine,
        HorsePower = car.HorsePower,
        Type = car.Type,
        CarDealerShipId = car.CarDealerShipId,
      };
    }
    
    public static Car ToCarFromCreateDto(this CreateCarRequest carDto , int CarDealerShipId)
    {
      return new Car
      {
        Company = carDto.Company,
        ModelName = carDto.ModelName,
        Year = carDto.Year,
        Color = carDto.Color,
        Images = carDto.Images,
        Description = carDto.Description,
        Price = carDto.Price,
        Fuel = carDto.Fuel,
        Transmission = carDto.Transmission,
        Mileage = carDto.Mileage,
        Engine = carDto.Engine,
        HorsePower = carDto.HorsePower,
        Type = carDto.Type,
        CarDealerShipId = CarDealerShipId,
      };
    }
        
  }
}