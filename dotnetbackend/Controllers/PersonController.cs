using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetbackend.Dtos.Person;
using dotnetbackend.IServices;
using dotnetbackend.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnetbackend.Controllers
{ [Route("api/person")]
  [ApiController]
  public class PersonController : ControllerBase
  {
    private readonly UserManager<Person> _userManager;
    private readonly ITokenService _tokenService;
    private readonly SignInManager<Person> _signInManager;
    public PersonController(UserManager<Person> userManager, ITokenService tokenService, SignInManager<Person> signInManager)
    {
      _userManager = userManager;
      _tokenService = tokenService;
      _signInManager = signInManager;
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
                Token = _tokenService.CreateToken(person),
                RegisterMessage = "Register succeded"
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


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
      try
      {
        if (loginDto == null || !ModelState.IsValid)
        {
          return BadRequest("Invalid login data.");
        }
        var person = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.UserName.ToLower());
        if (person == null)
        {
          return Unauthorized("Invalid username or password.");
        }
        var result = await _signInManager.CheckPasswordSignInAsync(person, loginDto.Password, false);

        if (!result.Succeeded)
        {
          return Unauthorized("Invalid username or password.");
        }
        return Ok(
          new NewPersonDto
          {
            id=person.Id,
            UserName = person.UserName,
            Email = person.Email,
            Token = _tokenService.CreateToken(person)
          });
        
      }catch (Exception ex)
      {
        return StatusCode(500, $"Internal server error: {ex.Message}");
      }
      
    }

  }
}