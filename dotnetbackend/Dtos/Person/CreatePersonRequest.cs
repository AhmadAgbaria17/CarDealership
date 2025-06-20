using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetbackend.Dtos.Person
{
  public class CreatePersonRequest
  {
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address format")]
    public String Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, ErrorMessage = "Password must be at least 6 characters long", MinimumLength = 6)]
    public String Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "First name is required")]
    [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters")]
    public String FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters")]
    public String LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone number is required")]
    [Phone(ErrorMessage = "Phone number must be a valid phone number")]
    public String Phone { get; set; } = string.Empty;
      
    }
}