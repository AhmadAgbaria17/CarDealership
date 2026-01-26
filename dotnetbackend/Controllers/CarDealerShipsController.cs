using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetbackend.Dtos.CarDealerShip;
using dotnetbackend.Extensions;
using dotnetbackend.Helpers;
using dotnetbackend.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnetbackend.Controllers
{
  [Route("api/car-dealer-ships")]
  [ApiController]
  public class CarDealerShipsController : ControllerBase
  {

    private readonly ICarDealerShipService _carDealerShipService;

    public CarDealerShipsController(ICarDealerShipService carDealerShipService)
    {
      _carDealerShipService = carDealerShipService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] CDHQueryObject? queryObject)
    {
      try
      {
        // Handle null queryObject
        if (queryObject == null)
        {
          queryObject = new CDHQueryObject();
        }

        if (!ModelState.IsValid)
        {
          return BadRequest(ModelState);
        }

        var carDealerShips = await _carDealerShipService.GetAllAsync(queryObject);
        if (carDealerShips == null || !carDealerShips.Any())
        {
          return Ok(new List<CarDealerShipDto>()); // Return empty list instead of NotFound
        }
        return Ok(carDealerShips);
      }
      catch (Exception ex)
      {
        // Log the exception (you might want to use ILogger here)
        Console.WriteLine($"Error in GetAll: {ex.Message}");
        Console.WriteLine($"Stack trace: {ex.StackTrace}");
        return StatusCode(500, new { Message = "An error occurred while fetching car dealer ships", Error = ex.Message });
      }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var carDealerShip = await _carDealerShipService.GetByIdAsync(id);
      if (carDealerShip is null)
      {
        return NotFound(new
        {
          Message = $"Car dealer ship with id {id} not found"
        });
      }
      return Ok(carDealerShip);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCarDealerShipRequest carDealerShipDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (carDealerShipDto == null)
      {
        return BadRequest("Car dealer ship data is required");
      }


      var username = User.GetUserName();
      Console.WriteLine(username);
      

      var carDealerShip = await _carDealerShipService.CreateAsync(carDealerShipDto , username);
      return CreatedAtAction(nameof(GetById), new { id = carDealerShip.Id }, carDealerShip);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCarDealerShipRequest carDealerShipDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (carDealerShipDto == null)
      {
        return BadRequest("Car dealer ship data is required");
      }
      var updatedCarDealerShip = await _carDealerShipService.UpdateAsync(id, carDealerShipDto);
      if (updatedCarDealerShip is null)
      {
        return NotFound(new
        {
          Message = $"Car dealer ship with id {id} not found"
        });
      }
      return Ok(updatedCarDealerShip);
    }

    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var deleted = await _carDealerShipService.DeleteAsync(id);
      if (!deleted)
      {
        return NotFound(new
        {
          Message = $"Car dealer ship with id {id} not found"
        });
      }
      return NoContent();
    }






  }
}