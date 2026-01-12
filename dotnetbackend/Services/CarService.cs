using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetbackend.Dtos.Car;
using dotnetbackend.Helpers;
using dotnetbackend.IRepository;
using dotnetbackend.IServices;
using dotnetbackend.Mappers;
using dotnetbackend.models;





namespace dotnetbackend.Services
{
  public class CarService : ICarService
  {
  
    private readonly ICarRepository _carRepository;
    private readonly ICloudinaryService _cloudinaryService;
    
    public CarService(ICarRepository carRepository, ICloudinaryService cloudinaryService)
    {
      _carRepository = carRepository;
      _cloudinaryService = cloudinaryService;
    }



    public async Task<CarDto?> AddCarAsync(int CarDealerShipId, CreateCarRequest carDto)
    {
      // If images are provided, upload them to Cloudinary first
      string[]? cloudinaryUrls = null;
      
      if (carDto.Images != null && carDto.Images.Length > 0)
      {
        try
        {
          // Upload all images to Cloudinary and get their URLs
          cloudinaryUrls = await _cloudinaryService.UploadImagesAsync(carDto.Images, "cars");
        }
        catch (Exception ex)
        {
          // Log the error (you might want to use a logger here)
          throw new Exception($"Failed to upload images to Cloudinary: {ex.Message}", ex);
        }
      }

      // Create a modified DTO with Cloudinary URLs instead of base64 strings
      var carDtoWithUrls = new CreateCarRequest
      {
        Company = carDto.Company,
        ModelName = carDto.ModelName,
        Year = carDto.Year,
        Color = carDto.Color,
        Images = cloudinaryUrls ?? Array.Empty<string>(), // Use Cloudinary URLs or empty array
        Description = carDto.Description,
        Price = carDto.Price,
        Fuel = carDto.Fuel,
        Transmission = carDto.Transmission,
        Mileage = carDto.Mileage,
        Engine = carDto.Engine,
        HorsePower = carDto.HorsePower,
        Type = carDto.Type
      };
      
      var car = carDtoWithUrls.ToCarFromCreateDto(CarDealerShipId);
      await _carRepository.AddCarAsync(car);
      return car.ToCarDto();
    }

    public async Task<bool> DeleteCarAsync(int id)
    {
      var car = await _carRepository.GetCarByIdAsync(id);
      if (car == null)
      {
        return false;
      }

      // Delete images from Cloudinary if they exist
      // Note: Image deletion failures won't block car deletion
      if (car.Images != null && car.Images.Length > 0)
      {
        foreach (var imageUrl in car.Images)
        {
          // Only delete if it's a Cloudinary URL
          if (!string.IsNullOrWhiteSpace(imageUrl) && imageUrl.Contains("cloudinary.com"))
          {
            // DeleteImageByUrlAsync never throws and always returns true
            // We don't need to check the result or catch exceptions
            Console.WriteLine($"Deleting image {imageUrl} from Cloudinary");
            _ = await _cloudinaryService.DeleteImageByUrlAsync(imageUrl);
          }
        }
      }

      return await _carRepository.DeleteCarAsync(id);
    }

    public async Task<List<CarDto>> GetAllCarsAsync(CQueryObject queryObject)
    {
      var cars = (await _carRepository.GetAllCarsAsync(queryObject))
        .Select(car => car.ToCarDto())
        .ToList();
      return cars;
    }

    public async Task<CarDto?> GetCarByIdAsync(int id)
    {
      return (await _carRepository.GetCarByIdAsync(id))?.ToCarDto();
    }

    public async Task<List<CarDto>> GetCarsByDealerShipIdAsync(int dealerShipId)
    {
      var cars = (await _carRepository.GetCarsByCarDealerShipIdAsync(dealerShipId))
        .Select(car => car.ToCarDto())
        .ToList();
      return cars;
      
    }

    public async Task<CarDto?> UpdateCarAsync(int id, UpdateCarRequest carDto)
    {
      var car = await _carRepository.GetCarByIdAsync(id);
      if (car == null)
      {
        return null;
      }

      // Separate new images (base64) from existing images (URLs)
      var existingImageUrls = new List<string>();
      var newBase64Images = new List<string>();

      if (carDto.Images != null && carDto.Images.Length > 0)
      {
        foreach (var image in carDto.Images)
        {
          // Check if it's a base64 string (new image) or a URL (existing image)
          if (!string.IsNullOrWhiteSpace(image) && 
              (image.StartsWith("data:image/") || image.StartsWith("/9j/") || 
               (image.Length > 100 && !image.Contains("http"))))
          {
            // It's a base64 image - needs to be uploaded
            newBase64Images.Add(image);
          }
          else if (!string.IsNullOrWhiteSpace(image) && image.Contains("cloudinary.com"))
          {
            // It's an existing Cloudinary URL - keep it
            existingImageUrls.Add(image);
          }
          else if (!string.IsNullOrWhiteSpace(image))
          {
            // Might be a base64 without data URL prefix
            newBase64Images.Add(image);
          }
        }
      }

      // Delete old images that are not in the new list
      if (car.Images != null && car.Images.Length > 0)
      {
        foreach (var oldImageUrl in car.Images)
        {
          // Only delete if it's not in the new list of images
          if (!existingImageUrls.Contains(oldImageUrl) && 
              !string.IsNullOrWhiteSpace(oldImageUrl) && 
              oldImageUrl.Contains("cloudinary.com"))
          {
            try
            {
              await _cloudinaryService.DeleteImageByUrlAsync(oldImageUrl);
            }
            catch (Exception ex)
            {
              // Log but don't fail the update
              System.Diagnostics.Debug.WriteLine($"Warning: Failed to delete old image {oldImageUrl} from Cloudinary: {ex.Message}");
            }
          }
        }
      }

      // Upload new images to Cloudinary
      string[]? newCloudinaryUrls = null;
      if (newBase64Images.Count > 0)
      {
        try
        {
          newCloudinaryUrls = await _cloudinaryService.UploadImagesAsync(newBase64Images.ToArray(), "cars");
        }
        catch (Exception ex)
        {
          throw new Exception($"Failed to upload new images to Cloudinary: {ex.Message}", ex);
        }
      }

      // Combine existing URLs with new URLs
      var allImageUrls = new List<string>();
      allImageUrls.AddRange(existingImageUrls);
      if (newCloudinaryUrls != null)
      {
        allImageUrls.AddRange(newCloudinaryUrls);
      }

      // Update the DTO with the final image URLs
      carDto.Images = allImageUrls.ToArray();
  
      var updatedCar = await _carRepository.UpdateCarAsync(id, carDto);
  
      return updatedCar?.ToCarDto();
    }
  }
}