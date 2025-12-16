using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetbackend.Dtos.Car
{
  public class CreateCarRequest
  {
    [Required(ErrorMessage = "Company is required")]
    public string Company { get; set; } = string.Empty;

    [Required(ErrorMessage = "Model name is required")]
    public string ModelName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Year is required")]
    [Range(1886, int.MaxValue, ErrorMessage = "Year must be a valid year")]
    public int Year { get; set; }

    [Required(ErrorMessage = "Color is required")]
    [StringLength(30, ErrorMessage = "Color cannot be longer than 30 characters")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Color must contain only letters and spaces")]
    public string Color { get; set; } = string.Empty;

    [Required(ErrorMessage = "Images URL is required")]
    [Url(ErrorMessage = "Images must be a valid URL")]
    public string[] Images { get; set; } = [];

    [Required(ErrorMessage = "Description is required")]
    [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Price is required")]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number")]
    public string Price { get; set; } = string.Empty;

    [Required(ErrorMessage = "Fuel type is required")]
    [StringLength(20, ErrorMessage = "Fuel type cannot be longer than 20 characters")]
    public string Fuel { get; set; } = string.Empty;

    [Required(ErrorMessage = "Transmission type is required")]
    [StringLength(20, ErrorMessage = "Transmission type cannot be longer than 20 characters")]
    public string Transmission { get; set; } = string.Empty;

    [Required(ErrorMessage = "Mileage is required")]
    [Range(0, double.MaxValue, ErrorMessage = "Mileage must be a positive number")]
    public string Mileage { get; set; } = string.Empty;

    [Required(ErrorMessage = "Engine is required")]
    [StringLength(50, ErrorMessage = "Engine cannot be longer than 50 characters")]
    public string Engine { get; set; } = string.Empty;

    [Required(ErrorMessage = "Horse power is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Horse power must be a positive number")]
    public string HorsePower { get; set; } = string.Empty;

    [Required(ErrorMessage = "Type is required")]
    [StringLength(30, ErrorMessage = "Type cannot be longer than 30 characters")]
    public string Type { get; set; } = string.Empty;

    }
}