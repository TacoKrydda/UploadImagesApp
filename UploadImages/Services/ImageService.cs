using Microsoft.EntityFrameworkCore;
using UploadImages.Context;
using UploadImages.Models;

namespace UploadImages.Services
{
    public class ImageService : IImageService
    {
        private readonly UploadImagesContext _context;
        private readonly IWebHostEnvironment _environment;

        public ImageService(UploadImagesContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<List<ImageModel>> GetAllImages()
        {
            return await _context.ImageModels.ToListAsync();
        }

        public async Task<List<string>> SearchTags(string term)
        {
            if (string.IsNullOrEmpty(term))
            {
                return new List<string>();
            }

            return await _context.TagModels
                .Where(tag => tag.Name.Contains(term))
                .Select(tag => tag.Name)
                .ToListAsync();
        }

        public async Task<ImageModel?> GetImageForEdit(int id)
        {
            return await _context.ImageModels
                .Include(img => img.ImageTagModels)
                .ThenInclude(it => it.Tag)
                .FirstOrDefaultAsync(img => img.Id == id);
        }

        public async void UpdateImage(ImageModel updatedImage, List<string> tags)
        {
            var image = await _context.ImageModels
                .Include(img => img.ImageTagModels)
                .ThenInclude(it => it.Tag)
                .FirstOrDefaultAsync(img => img.Id == updatedImage.Id);

            if (image == null)
            {
                return;
            }

            image.Description = updatedImage.Description;

            _context.ImageTags.RemoveRange(image.ImageTagModels);

            foreach (var tagName in tags)
            {
                var tag = await _context.TagModels.FirstOrDefaultAsync(t => t.Name == tagName)
                          ?? new Tagmodel { Name = tagName };

                if (tag.Id == 0)
                {
                    _context.TagModels.Add(tag);
                }

                await _context.ImageTags.AddAsync(new ImageTagModel { ImageId = image.Id, TagId = tag.Id });
            }

            await _context.SaveChangesAsync();
        }

        public async Task<ImageModel> UploadImageAsync(IFormFile file, string description, List<string> tags)
        {
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "images");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var imageModel = new ImageModel
            {
                Name = uniqueFileName,
                Url = $"/images/{uniqueFileName}",
                Description = description,
                UploadDate = DateTime.Now,
                ImageTagModels = new List<ImageTagModel>()
            };

            foreach (var tagName in tags)
            {
                var tag = _context.TagModels.FirstOrDefault(t => t.Name == tagName) ?? new Tagmodel { Name = tagName };

                if (tag.Id == 0)
                {
                    _context.TagModels.Add(tag); // Skapa en ny tag om den inte finns
                }

                // Skapa koppling mellan bilden och taggen
                var imageTagModel = new ImageTagModel
                {
                    Image = imageModel,
                    Tag = tag
                };

                imageModel.ImageTagModels.Add(imageTagModel);
            }

            await _context.ImageModels.AddAsync(imageModel);
            await _context.SaveChangesAsync();

            return imageModel;
        }

        public async Task<List<ImageModel>> SearchImagesAsync(List<string> tags)
        {
            return await _context.ImageModels
                .Where(image => image.ImageTagModels.Any(it => tags.Contains(it.Tag.Name)))
                .ToListAsync();
        }

        public async Task<ImageModel?> GetImageByIdAsync(int id)
        {
            return await _context.ImageModels
                .Include(i => i.ImageTagModels)
                .ThenInclude(it => it.Tag)
                .FirstOrDefaultAsync(img => img.Id == id);
        }

        public async Task DeleteImageAsync(int id)
        {
            var image = await _context.ImageModels.FirstOrDefaultAsync(img => img.Id == id);

            if (image != null)
            {
                _context.ImageModels.Remove(image);
                await _context.SaveChangesAsync();

                // Ta bort själva bildfilen från filsystemet
                var filePath = Path.Combine(_environment.WebRootPath, "images", image.Name);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }
    }
}
