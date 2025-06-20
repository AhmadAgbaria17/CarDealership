using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using dotnetbackend.Dtos.Car;

namespace dotnetbackend.Dtos.CarDealerShip
{
  public class CreateCarDealerShipRequest
  {

    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
    public String Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "City is required")]
    [StringLength(100, ErrorMessage = "City cannot be longer than 100 characters")]
    public String City { get; set; } = string.Empty;

    [Required(ErrorMessage = "Address is required")]
    [StringLength(200, ErrorMessage = "Address cannot be longer than 200 characters")]
    public String Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "Coordinates are required")]
    [MinLength(2, ErrorMessage = "Coordinates must contain exactly 2 elements")]
    public int[] Coordinates { get; set; } = new int[2];

    [Required(ErrorMessage = "Phone number is required")]
    [Phone(ErrorMessage = "Phone number must be a valid phone number")]
    [StringLength(15, ErrorMessage = "Phone number cannot be longer than 15 characters")]
    public String Phone { get; set; } = string.Empty;

    public List<CarDto> Cars { get; set; } = new List<CarDto>();

    }
}