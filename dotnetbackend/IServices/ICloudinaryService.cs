using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetbackend.IServices
{
  public interface ICloudinaryService
  {
    /// <summary>
    /// Uploads a single image (base64 string) to Cloudinary and returns the secure URL
    /// </summary>
    /// <param name="base64Image">Base64 encoded image string</param>
    /// <param name="folder">Optional folder name in Cloudinary (e.g., "cars")</param>
    /// <returns>Secure URL of the uploaded image</returns>
    Task<string> UploadImageAsync(string base64Image, string? folder = null);

    /// <summary>
    /// Uploads multiple images (base64 strings) to Cloudinary and returns their secure URLs
    /// </summary>
    /// <param name="base64Images">Array of base64 encoded image strings</param>
    /// <param name="folder">Optional folder name in Cloudinary (e.g., "cars")</param>
    /// <returns>Array of secure URLs of the uploaded images</returns>
    Task<string[]> UploadImagesAsync(string[] base64Images, string? folder = null);

    /// <summary>
    /// Deletes an image from Cloudinary using its public ID
    /// </summary>
    /// <param name="publicId">Public ID of the image in Cloudinary</param>
    /// <returns>True if deletion was successful</returns>
    Task<bool> DeleteImageAsync(string publicId);

    /// <summary>
    /// Deletes an image from Cloudinary using its URL (extracts public ID from URL)
    /// </summary>
    /// <param name="imageUrl">Cloudinary URL of the image</param>
    /// <returns>True if deletion was successful</returns>
    Task<bool> DeleteImageByUrlAsync(string imageUrl);

    /// <summary>
    /// Extracts the public ID from a Cloudinary URL
    /// </summary>
    /// <param name="cloudinaryUrl">Full Cloudinary URL</param>
    /// <returns>Public ID of the image</returns>
    string ExtractPublicIdFromUrl(string cloudinaryUrl);
  }
}
