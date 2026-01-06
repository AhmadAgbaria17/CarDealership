using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetbackend.Data;
using dotnetbackend.Dtos.Car;
using dotnetbackend.Helpers;
using dotnetbackend.IServices;
using dotnetbackend.Mappers;
using dotnetbackend.Services;
using dotnetbackend.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace dotnetbackend.Controllers
{
  [Route("api/car")]
  [ApiController]
  public class CarController : ControllerBase
  {
    private readonly ICarService _carService;
    private readonly ICarDealerShipService _carDealerShipService;

    public CarController(ICarService carService, ICarDealerShipService carDealerShipService)
    {
      _carService = carService;
      _carDealerShipService = carDealerShipService;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] CQueryObject queryObject)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var cars = await _carService.GetAllCarsAsync(queryObject);
      if (cars == null || !cars.Any())
      {
        return NotFound("No cars found");
      }
      return Ok(cars);
    }


    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var car = await _carService.GetCarByIdAsync(id);
      if (car is null)
      {
        return NotFound($"Car with id {id} not found");
      }
      return Ok(car);
    }

    [Authorize]
    [HttpPost("{CarDealerShipId:int}")]
    public async Task<IActionResult> Create([FromRoute] int CarDealerShipId, [FromBody] CreateCarRequest carDto)
    {
    if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var isDealerShipExists = await _carDealerShipService.IsCarDealerShipExistsAsync(CarDealerShipId);
      if (!isDealerShipExists)
      {
        return NotFound($"Car dealer ship with id {CarDealerShipId} not found");
      }

      if (carDto == null)
      {
        return BadRequest("Car data is required");
      }

      var car = await _carService.AddCarAsync(CarDealerShipId,carDto);
      return CreatedAtAction(nameof(GetById), new { id = car?.Id }, car);
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCarRequest carDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var updatedcar = await _carService.UpdateCarAsync(id, carDto);
      if (updatedcar is null)
      {
        return NotFound($"Car with id {id} not found");
      }
      return Ok(updatedcar);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var isDeleted = await _carService.DeleteCarAsync(id);
      if (!isDeleted)
      {
        return NotFound($"Car with id {id} not found");
      }
      return NoContent();
    }

    
    [HttpGet("dealer/{dealerShipId:int}")]
    public async Task<IActionResult> GetCarsByDealerShipId([FromRoute] int dealerShipId)
    {

      if (!ModelState.IsValid)
        {
          return BadRequest(ModelState);
        }

      var cars = await _carService.GetCarsByDealerShipIdAsync(dealerShipId);
      if (cars == null || !cars.Any())
      {
        return NotFound($"No cars found for dealer ship with id {dealerShipId}");
      }
      return Ok(cars);
    }
  }
}