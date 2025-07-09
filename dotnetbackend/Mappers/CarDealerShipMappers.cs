using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetbackend.Dtos.CarDealerShip;
using dotnetbackend.models;
using dotnetbackend.Repository;

namespace dotnetbackend.Mappers
{
  public static class CarDealerShipMappers
  {
    public static CarDealerShipDto ToCarDealerShipDto(this CarDealerShips carDealerShips)
    {
      return new CarDealerShipDto
      {
        Id = carDealerShips.Id,
        Name = carDealerShips.Name,
        City = carDealerShips.City,
        Address = carDealerShips.Address,
        Coordinates = carDealerShips.Coordinates,
        Phone = carDealerShips.Phone,
        CreatedBy = carDealerShips.Person?.UserName ?? "Unknown",
        Cars = carDealerShips.Cars.Select(car => car.ToCarDto()).ToList()
      };
    }

    public static CarDealerShips ToCarDealerShipFromCreateDto(this CreateCarDealerShipRequest carDealerShipDto) {
      return new CarDealerShips
      {
        Name = carDealerShipDto.Name,
        City = carDealerShipDto.City,
        Address = carDealerShipDto.Address,
        Coordinates = carDealerShipDto.Coordinates,
        Phone = carDealerShipDto.Phone
        };
    }
      
  }
}