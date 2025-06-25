using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetbackend.Dtos.Person;
using dotnetbackend.IServices;
using dotnetbackend.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace dotnetbackend.Controllers
{   [Route("api/person")]
  [ApiController]
  public class PersonController : ControllerBase
  {
    private readonly UserManager<Person> _userManager;
    private readonly ITokenService _tokenService;
    public PersonController(UserManager<Person> userManager, ITokenService tokenService)
    {
      _userManager = userManager;
      _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
      try
      {
        if (registerDto == null || !ModelState.IsValid)
        {
          return BadRequest("Invalid registration data.");
        }

        var person = new Person
        {
          UserName = registerDto.UserName,
          Email = registerDto.Email
        };

        var createdUser = await _userManager.CreateAsync(person, registerDto.Password);

        if (createdUser.Succeeded)
        {
          var roleResult = await _userManager.AddToRoleAsync(person, "User");
          if (roleResult.Succeeded)
          {
            return Ok(
              new NewPersonDto
              {
                UserName = person.UserName,
                Email = person.Email,
                Token = _tokenService.CreateToken(person)
              });
          }
          else
          {
            return BadRequest($"Failed to assign role: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
          }
        }
        else
        {
          return BadRequest($"User creation failed: {string.Join(", ", createdUser.Errors.Select(e => e.Description))}");
        }
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Internal server error: {ex.Message}");
      }
    
    }


        
    }
}