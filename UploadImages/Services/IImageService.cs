using UploadImages.Models;

namespace UploadImages.Services
{
    public interface IImageService
    {
        Task DeleteImageAsync(int id);
        Task<List<ImageModel>> GetAllImages();
        Task<ImageModel?> GetImageByIdAsync(int id);
        Task<ImageModel?> GetImageForEdit(int id);
        Task<List<ImageModel>> SearchImagesAsync(List<string> tags);
        Task<List<string>> SearchTags(string term);
        void UpdateImage(ImageModel updatedImage, List<string> tags);
        Task<ImageModel> UploadImageAsync(IFormFile file, string description, List<string> tags);
    }
}