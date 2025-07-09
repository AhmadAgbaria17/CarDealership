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
using dotnetbackend.models;
using Microsoft.AspNetCore.Identity;

namespace dotnetbackend.Services
{
  public class CarDealerShipService : ICarDealerShipService
  {

    private readonly ICarDealerShipsRepository _carDealerShipsRepository;
    private readonly UserManager<Person> _userManager;


    public CarDealerShipService(ICarDealerShipsRepository carDealerShipsRepository,
      UserManager<Person> userManager)
    {
      _carDealerShipsRepository = carDealerShipsRepository;
      _userManager = userManager;
    }


    public async Task<CarDealerShipDto> CreateAsync(CreateCarDealerShipRequest carDealerShipDto , string username)
    {
      var Person = await _userManager.FindByNameAsync(username);

      if (Person?.Id == null)
      {
        throw new Exception("User not found");
      }

      var carDealerShip = carDealerShipDto.ToCarDealerShipFromCreateDto();
      carDealerShip.PersonId = Person.Id;
      
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