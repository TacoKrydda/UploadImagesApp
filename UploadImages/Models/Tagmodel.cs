namespace UploadImages.Models
{
    public class Tagmodel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<ImageTagModel>? ImageTagModels { get; set; } = [];
    }
}
