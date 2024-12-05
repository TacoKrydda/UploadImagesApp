namespace UploadImages.Models
{
    public class ImageTagModel
    {
        public int ImageId { get; set; }
        public ImageModel? Image { get; set; }
        public int TagId { get; set; }
        public Tagmodel? Tag { get; set; }
    }
}
