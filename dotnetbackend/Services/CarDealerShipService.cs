using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using dotnetbackend.Dtos.CarDealerShip;
using dotnetbackend.Helpers;
using dotnetbackend.IRepository;
using dotnetbackend.IServices;
using dotnetbackend.Mappers;

namespace dotnetbackend.Services
{
  public class CarDealerShipService : ICarDealerShipService
  {

    private readonly ICarDealerShipsRepository _carDealerShipsRepository;


    public CarDealerShipService(ICarDealerShipsRepository carDealerShipsRepository)
    {
      _carDealerShipsRepository = carDealerShipsRepository;
    }


    public async Task<CarDealerShipDto> CreateAsync(CreateCarDealerShipRequest carDealerShipDto)
    {
      var carDealerShip = carDealerShipDto.ToCarDealerShipFromCreateDto();
      await _carDealerShipsRepository.AddCarDealerShipAsync(carDealerShip);
      return carDealerShip.ToCarDealerShipDto();
      
    }

    public async Task<bool> DeleteAsync(int id)
    {
      return await _carDealerShipsRepository.DeleteCarDealerShipAsync(id);
    }

    public async Task<List<CarDealerShipDto>> GetAllAsync(CDHQueryObject queryObject)
    {
      var carDealerShips = (await _carDealerShipsRepository.GetAllCarDealerShipsAsync(queryObject))
        .Select(c => c.ToCarDealerShipDto())
        .ToList();
        return carDealerShips;
    }

    public async Task<CarDealerShipDto?> GetByIdAsync(int id)
    {
      var carDealerShip = (await _carDealerShipsRepository.GetCarDealerShipByIdAsync(id))
        ?.ToCarDealerShipDto();
        return carDealerShip;
    }

    public Task<bool> IsCarDealerShipExistsAsync(int id)
    {
      return _carDealerShipsRepository.IsCarDealerShipExistsAsync(id);
    }

    public async Task<CarDealerShipDto?> UpdateAsync(int id, UpdateCarDealerShipRequest carDealerShipDto)
    {
      var updatedCarDealerShip = await _carDealerShipsRepository.UpdateCarDealerShipAsync(id, carDealerShipDto);
      return updatedCarDealerShip?.ToCarDealerShipDto();
    }
  }
}