using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetbackend.Data;
using dotnetbackend.Dtos.CarDealerShip;
using dotnetbackend.Helpers;
using dotnetbackend.IRepository;
using dotnetbackend.models;
using Microsoft.EntityFrameworkCore;

namespace dotnetbackend.Repository
{
  public class CarDealerShipsRepository : ICarDealerShipsRepository
  {

    private readonly ApplicationDbContext _context;
    public CarDealerShipsRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<CarDealerShips> AddCarDealerShipAsync(CarDealerShips carDealerShip)
    {
      await _context.CarDealerShips.AddAsync(carDealerShip);
      await _context.SaveChangesAsync();
      return carDealerShip;
      
    }

    public async Task<bool> DeleteCarDealerShipAsync(int id)
    {
      var carDealerShip = await _context.CarDealerShips.FindAsync(id);
      if (carDealerShip is null)
      {
        return false;
      }
      _context.CarDealerShips.Remove(carDealerShip);
      await _context.SaveChangesAsync();
      return true;
      
    }

    public async Task<List<CarDealerShips>> GetAllCarDealerShipsAsync(CDHQueryObject queryObject)
    {
      var CarDealerShip = _context.CarDealerShips.Include(c => c.Cars).Include(a => a.Person).AsQueryable();

      if (!string.IsNullOrEmpty(queryObject.Name))
      {
        CarDealerShip = CarDealerShip.Where(c => c.Name.Contains(queryObject.Name));
      }
      if (!string.IsNullOrEmpty(queryObject.City))
      {
        CarDealerShip = CarDealerShip.Where(c => c.City.Contains(queryObject.City));
      }

      if (!string.IsNullOrWhiteSpace(queryObject.SortBy))
      {
        if (queryObject.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
        {
          CarDealerShip = queryObject.IsDescending
            ? CarDealerShip.OrderByDescending(c => c.Name)
            : CarDealerShip.OrderBy(c => c.Name);
        }
      }

      if (queryObject.PageNumber > 0 && queryObject.PageSize > 0)
      {
        CarDealerShip = CarDealerShip
          .Skip((queryObject.PageNumber - 1) * queryObject.PageSize)
          .Take(queryObject.PageSize);
      }

      return await CarDealerShip.ToListAsync();
    }

    public async Task<CarDealerShips> GetCarDealerShipByIdAsync(int id)
    {
      return await _context.CarDealerShips.Include(c => c.Cars).Include(a=>a.Person).FirstOrDefaultAsync(i => i.Id == id)
             ?? throw new KeyNotFoundException($"CarDealerShip with id {id} not found."); 
             
    }

    public Task<bool> IsCarDealerShipExistsAsync(int id)
    {
      return _context.CarDealerShips.AnyAsync(c => c.Id == id);
    }

    public async Task<CarDealerShips> UpdateCarDealerShipAsync(int id, UpdateCarDealerShipRequest carDealerShip)
    {
      var existingCarDealerShip = await _context.CarDealerShips.FindAsync(id);
      if (existingCarDealerShip is null)
      {
        throw new KeyNotFoundException($"CarDealerShip with id {id} not found.");
      }

      existingCarDealerShip.Name = carDealerShip.Name;
      existingCarDealerShip.City = carDealerShip.City;
      existingCarDealerShip.Address = carDealerShip.Address;
        existingCarDealerShip.Coordinates = carDealerShip.Coordinates;
      existingCarDealerShip.Phone = carDealerShip.Phone;

      _context.CarDealerShips.Update(existingCarDealerShip);
      await _context.SaveChangesAsync();
      
      return existingCarDealerShip;
    }
  }
}