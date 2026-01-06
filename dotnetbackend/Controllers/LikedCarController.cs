using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetbackend.Extensions;
using dotnetbackend.IRepository;
using dotnetbackend.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace dotnetbackend.Controllers
{
  [Route("api/likedcars")]
  [ApiController]
  public class LikedCarController : ControllerBase
  {
    private readonly UserManager<Person> _userManager;
    private readonly ICarRepository _carRepository;
    private readonly ILikedCarRepository _likedCarRepository;

    public LikedCarController(UserManager<Person> userManager,
    ICarRepository carRepository, ILikedCarRepository likedCarRepository)
    {
      _userManager = userManager;
      _carRepository = carRepository;
      _likedCarRepository = likedCarRepository;

    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetLikedCars()
    {
      var person = User.GetUserName();
      var user = await _userManager.FindByNameAsync(person);
      var likedCars = await _likedCarRepository.GetPersonLikedCar(user);

      return Ok(likedCars);

    }


    [HttpPost("{carId}")]
    [Authorize]
    public async Task<IActionResult> LikeCar([FromRoute] int carId)
    {
      var username = User.GetUserName();
      var user = await _userManager.FindByNameAsync(username);
      Console.WriteLine(user);
      if (user == null)
        return Unauthorized("User not found");

      var car = await _carRepository.GetCarByIdAsync(carId);
      if (car == null)
      {
        return NotFound("Car not found.");
      }

      var result = await _likedCarRepository.LikeCarAsync(user, carId);
      if (!result)
      {
        return BadRequest("Car already liked or an error occurred.");
      }
      return Ok("Car liked successfully.");
    }


    [HttpDelete("{carId}")]
    [Authorize]
    public async Task<IActionResult> UnlikeCar([FromRoute] int carId)
    {
      var username = User.GetUserName();
      var user = await _userManager.FindByNameAsync(username);

      if (user == null)
        return Unauthorized("User not found");

      var result = await _likedCarRepository.UnlikeCarAsync(user, carId);
      if (!result)
        return NotFound("Car not found or not liked by user.");

      return Ok(new { Message = "Car unliked successfully." });
  }
      

    

  }
}