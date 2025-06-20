using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetbackend.Dtos.CarDealerShip;
using dotnetbackend.Helpers;

namespace dotnetbackend.IServices
{
  public interface ICarDealerShipService
  {
    Task<List<CarDealerShipDto>> GetAllAsync(CDHQueryObject queryObject);
    Task<CarDealerShipDto?> GetByIdAsync(int id);
    Task<CarDealerShipDto> CreateAsync(CreateCarDealerShipRequest carDealerShipDto);
    Task<CarDealerShipDto?> UpdateAsync(int id, UpdateCarDealerShipRequest carDealerShipDto);
    Task<bool> DeleteAsync(int id);
    Task<bool> IsCarDealerShipExistsAsync(int id);
    
        
    }
}