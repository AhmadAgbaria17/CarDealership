using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetbackend.Dtos.CarDealerShip;
using dotnetbackend.Helpers;
using dotnetbackend.models;

namespace dotnetbackend.IRepository
{
  public interface ICarDealerShipsRepository
  {
    Task<List<CarDealerShips>> GetAllCarDealerShipsAsync(CDHQueryObject queryObject);
    Task<CarDealerShips> GetCarDealerShipByIdAsync(int id);
    Task<CarDealerShips> AddCarDealerShipAsync(CarDealerShips carDealerShip);
    Task<CarDealerShips> UpdateCarDealerShipAsync(int id, UpdateCarDealerShipRequest carDealerShip);
    Task<bool> DeleteCarDealerShipAsync(int id);
    Task<bool> IsCarDealerShipExistsAsync(int id);
        
    }
}