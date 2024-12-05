using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UploadImages.Context;
using UploadImages.Models;
using UploadImages.Services;

namespace UploadImages.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly UploadImagesContext _context;
        private readonly IImageService _service;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment environment, UploadImagesContext context, IImageService service)
        {
            _logger = logger;
            _environment = environment;
            _context = context;
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: Visa formulär för bilduppladdning
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(List<IFormFile> files, [FromForm] string description, [FromForm] string tags)
        {
            if (files == null || !files.Any())
            {
                ViewBag.Message = "Please select at least one file.";
                return View();
            }

            var tagsList = string.IsNullOrEmpty(tags)
                ? new List<string>()
                : tags.Split(',').Select(tag => tag.Trim()).ToList();

            ImageModel? lastUploadedImage = null;

            foreach (var file in files)
            {
                lastUploadedImage = await _service.UploadImageAsync(file, description, tagsList);
            }

            if (lastUploadedImage != null)
            {
                return RedirectToAction("Details", new { id = lastUploadedImage.Id });
            }

            ViewBag.Message = "No valid images were uploaded.";
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Uploaded()
        {
            var uploadedFiles = await _service.GetAllImages();
            return View(uploadedFiles);
        }


        public async Task<IActionResult> Details(int id)
        {
            var image = await _service.GetImageByIdAsync(id);

            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        [HttpGet]
        public async Task<IActionResult> Search(List<string> query)
        {
            if (query == null || !query.Any())
            {
                ViewBag.Message = "No tags selected.";
                return View("Uploaded", _context.ImageModels.ToList());
            }

            var images = await _service.SearchImagesAsync(query);

            ViewBag.SelectedTags = query;

            return View("Uploaded", images);
        }

        [HttpGet]
        public async Task<JsonResult> GetTags(string term)
        {
            var tags = await _service.SearchTags(term);
            return Json(tags);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var image = await _service.GetImageForEdit(id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        [HttpPost]
        public IActionResult Edit(ImageModel updatedImage, string tags)
        {
            var tagsList = string.IsNullOrEmpty(tags)
                ? new List<string>()
                : tags.Split(',').Select(tag => tag.Trim()).ToList();

            _service.UpdateImage(updatedImage, tagsList);
            return RedirectToAction("Uploaded");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteImageAsync(id);
            return RedirectToAction("Uploaded");
        }

        public IActionResult ConfirmDelete(int id)
        {
            var image = _context.ImageModels.FirstOrDefault(img => img.Id == id);

            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
