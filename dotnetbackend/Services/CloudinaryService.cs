using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using dotnetbackend.IServices;
using Microsoft.Extensions.Configuration;

namespace dotnetbackend.Services
{
  public class CloudinaryService : ICloudinaryService
  {
    private readonly Cloudinary _cloudinary;
    private readonly IConfiguration _configuration;

    public CloudinaryService(IConfiguration configuration)
    {
      _configuration = configuration;
      
      // Initialize Cloudinary account with credentials from appsettings.json
      var account = new Account(
        _configuration["Cloudinary:CloudName"],
        _configuration["Cloudinary:ApiKey"],
        _configuration["Cloudinary:ApiSecret"]
      );

      _cloudinary = new Cloudinary(account);
    }

    public async Task<string> UploadImageAsync(string base64Image, string? folder = null)
    {
      try
      {
        // Remove data URL prefix if present (e.g., "data:image/jpeg;base64,")
        var base64Data = base64Image.Contains(",") 
          ? base64Image.Split(',')[1] 
          : base64Image;

        // Convert base64 string to byte array
        byte[] imageBytes = Convert.FromBase64String(base64Data);

        // Create upload parameters
        var uploadParams = new ImageUploadParams()
        {
          File = new FileDescription(Guid.NewGuid().ToString(), new System.IO.MemoryStream(imageBytes)),
          Folder = folder ?? "cars", // Default folder is "cars"
          Overwrite = false
          // ResourceType is automatically set to Image for ImageUploadParams
        };

        // Upload to Cloudinary
        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
        {
          return uploadResult.SecureUrl.ToString();
        }
        else
        {
          throw new Exception($"Cloudinary upload failed: {uploadResult.Error?.Message}");
        }
      }
      catch (Exception ex)
      {
        throw new Exception($"Error uploading image to Cloudinary: {ex.Message}", ex);
      }
    }

    public async Task<string[]> UploadImagesAsync(string[] base64Images, string? folder = null)
    {
      if (base64Images == null || base64Images.Length == 0)
      {
        return Array.Empty<string>();
      }

      try
      {
        // Upload all images in parallel for better performance
        var uploadTasks = base64Images.Select(image => UploadImageAsync(image, folder));
        var uploadedUrls = await Task.WhenAll(uploadTasks);

        return uploadedUrls;
      }
      catch (Exception ex)
      {
        throw new Exception($"Error uploading images to Cloudinary: {ex.Message}", ex);
      }
    }

    public async Task<bool> DeleteImageAsync(string publicId)
    {
      try
      {
        if (string.IsNullOrWhiteSpace(publicId))
        {
          System.Diagnostics.Debug.WriteLine("Warning: Attempted to delete image with empty public ID");
          return false;
        }

        var deleteParams = new DeletionParams(publicId)
        {
          ResourceType = ResourceType.Image
        };
        Console.WriteLine($"Delete Params: {deleteParams}");
        var result = await _cloudinary.DestroyAsync(deleteParams);

        // Cloudinary returns "ok" if deleted, "not found" if image doesn't exist
        // Both are acceptable - the image is gone either way
        if (result.Result == "ok" || result.Result == "not found")
        {
          return true;
        }

        System.Diagnostics.Debug.WriteLine($"Warning: Cloudinary deletion returned unexpected result: {result.Result} for public ID: {publicId}");
        return false;
      }
      catch (Exception ex)
      {
        // Log but don't throw - let the caller decide
        System.Diagnostics.Debug.WriteLine($"Warning: Exception deleting image from Cloudinary (public ID: {publicId}): {ex.Message}");
        return false;
      }
    }

    public async Task<bool> DeleteImageByUrlAsync(string imageUrl)
    {
      try
      {
        var publicId = ExtractPublicIdFromUrl(imageUrl);

        Console.WriteLine($"Public ID: {publicId}");
        // If extraction failed, return true anyway to not block the operation
        if (string.IsNullOrWhiteSpace(publicId))
        {
          System.Diagnostics.Debug.WriteLine($"Warning: Could not extract public ID from URL: {imageUrl}. Skipping deletion.");
          return true; // Return true to not block car deletion
        }
        
        var result = await DeleteImageAsync(publicId);
        
        // Always return true to not block car deletion, even if deletion failed
        return true;
      }
      catch (Exception ex)
      {
        // Log the error but don't throw - let the caller decide what to do
        System.Diagnostics.Debug.WriteLine($"Warning: Exception deleting image {imageUrl} from Cloudinary: {ex.Message}");
        // Return true instead of false to not block car deletion
        return true;
      }
    }

    public string ExtractPublicIdFromUrl(string cloudinaryUrl)
    {
      try
      {
        // Cloudinary URL format examples:
        // https://res.cloudinary.com/{cloud_name}/image/upload/{version}/{folder}/{public_id}.{format}
        // https://res.cloudinary.com/{cloud_name}/image/upload/{transformations}/{version}/{folder}/{public_id}.{format}
        // We need to extract: {folder}/{public_id} (without version, transformations, and format)
        
        if (string.IsNullOrWhiteSpace(cloudinaryUrl))
        {
          System.Diagnostics.Debug.WriteLine("Warning: Attempted to extract public ID from null or empty URL");
          return string.Empty; // Return empty instead of throwing
        }

        // Check if it's a Cloudinary URL
        if (!cloudinaryUrl.Contains("cloudinary.com"))
        {
          // If it's not a Cloudinary URL, assume it's already a public ID
          return cloudinaryUrl;
        }

        // Find the position after "/upload/"
        var uploadIndex = cloudinaryUrl.IndexOf("/upload/");
        if (uploadIndex == -1)
        {
          System.Diagnostics.Debug.WriteLine($"Warning: Invalid Cloudinary URL format - missing /upload/ in: {cloudinaryUrl}");
          return string.Empty; // Return empty instead of throwing
        }

        // Get the part after "/upload/"
        var afterUpload = cloudinaryUrl.Substring(uploadIndex + "/upload/".Length);

        // Remove query parameters if present
        var queryIndex = afterUpload.IndexOf('?');
        if (queryIndex > 0)
        {
          afterUpload = afterUpload.Substring(0, queryIndex);
        }

        // Split by '/' to handle transformations and version
        var parts = afterUpload.Split('/', StringSplitOptions.RemoveEmptyEntries);
        
        if (parts.Length == 0)
        {
          System.Diagnostics.Debug.WriteLine($"Warning: Invalid Cloudinary URL format - no path after /upload/ in: {cloudinaryUrl}");
          return string.Empty; // Return empty instead of throwing
        }

        // Find the version part (starts with 'v' followed by digits)
        int versionIndex = -1;
        for (int i = 0; i < parts.Length; i++)
        {
          var part = parts[i];
          if (part.Length > 1 && part[0] == 'v' && part.Substring(1).All(char.IsDigit))
          {
            versionIndex = i;
            break;
          }
        }

        // Everything after the version is the public ID path
        string publicIdPath;
        if (versionIndex >= 0 && versionIndex < parts.Length - 1)
        {
          // Public ID is everything after the version
          publicIdPath = string.Join("/", parts.Skip(versionIndex + 1));
        }
        else
        {
          // No version found - might have transformations or direct path
          // Find the first part that looks like a folder/image (contains a dot for extension)
          int imageIndex = -1;
          for (int i = 0; i < parts.Length; i++)
          {
            if (parts[i].Contains('.'))
            {
              imageIndex = i;
              break;
            }
          }
          
          if (imageIndex >= 0)
          {
            // Public ID is from this index onwards (includes folder)
            publicIdPath = string.Join("/", parts.Skip(imageIndex));
          }
          else
          {
            // No extension found - assume all parts after potential transformations are the public ID
            // Skip transformation-like patterns (contain underscores, commas, or are single letters)
            int startIndex = 0;
            for (int i = 0; i < parts.Length; i++)
            {
              // If it looks like a folder name (no special chars) or has a dot, start from here
              if (!parts[i].Contains('_') && !parts[i].Contains(',') && parts[i].Length > 1)
              {
                startIndex = i;
                break;
              }
            }
            publicIdPath = string.Join("/", parts.Skip(startIndex));
          }
        }

        // Remove file extension
        var lastDot = publicIdPath.LastIndexOf('.');
        if (lastDot > 0)
        {
          publicIdPath = publicIdPath.Substring(0, lastDot);
        }

        if (string.IsNullOrWhiteSpace(publicIdPath))
        {
          System.Diagnostics.Debug.WriteLine($"Warning: Could not extract public ID from URL: {cloudinaryUrl}");
          return string.Empty; // Return empty instead of throwing
        }

        return publicIdPath;
      }
      catch (Exception ex)
      {
        // Log but don't throw - return empty string instead
        System.Diagnostics.Debug.WriteLine($"Warning: Exception extracting public ID from URL '{cloudinaryUrl}': {ex.Message}");
        return string.Empty;
      }
    }
  }
}
