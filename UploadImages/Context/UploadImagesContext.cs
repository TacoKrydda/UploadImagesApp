using Microsoft.EntityFrameworkCore;
using UploadImages.Models;

namespace UploadImages.Context
{
    public class UploadImagesContext : DbContext
    {
        public UploadImagesContext(DbContextOptions<UploadImagesContext> options) : base(options)
        {

        }

        public DbSet<ImageModel> ImageModels { get; set; }
        public DbSet<Tagmodel> TagModels { get; set; }
        public DbSet<ImageTagModel> ImageTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ImageTagModel>()
                .HasKey(it => new { it.ImageId, it.TagId });
            modelBuilder.Entity<ImageTagModel>()
                .HasOne(it => it.Image)
                .WithMany(i => i.ImageTagModels)
                .HasForeignKey(it => it.ImageId);
            modelBuilder.Entity<ImageTagModel>()
                .HasOne(it => it.Tag)
                .WithMany(t => t.ImageTagModels)
                .HasForeignKey(it => it.TagId);
        }
    }
}
