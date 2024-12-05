namespace UploadImages.Models
{
    public class ImageModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public List<Tagmodel> Tags { get; set; } = [];
        public DateTime UploadDate { get; set; } = DateTime.Now;

        public ICollection<ImageTagModel>? ImageTagModels { get; set; } = [];
    }
}
