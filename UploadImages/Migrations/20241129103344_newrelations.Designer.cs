﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UploadImages.Context;

#nullable disable

namespace UploadImages.Migrations
{
    [DbContext(typeof(UploadImagesContext))]
    [Migration("20241129103344_newrelations")]
    partial class newrelations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("UploadImages.Models.ImageModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ImageModels");
                });

            modelBuilder.Entity("UploadImages.Models.ImageTagModel", b =>
                {
                    b.Property<int>("ImageId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("ImageId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("ImageTags");
                });

            modelBuilder.Entity("UploadImages.Models.Tagmodel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ImageModelId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ImageModelId");

                    b.ToTable("TagModels");
                });

            modelBuilder.Entity("UploadImages.Models.ImageTagModel", b =>
                {
                    b.HasOne("UploadImages.Models.ImageModel", "Image")
                        .WithMany("ImageTagModels")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UploadImages.Models.Tagmodel", "Tag")
                        .WithMany("ImageTagModels")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Image");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("UploadImages.Models.Tagmodel", b =>
                {
                    b.HasOne("UploadImages.Models.ImageModel", null)
                        .WithMany("Tags")
                        .HasForeignKey("ImageModelId");
                });

            modelBuilder.Entity("UploadImages.Models.ImageModel", b =>
                {
                    b.Navigation("ImageTagModels");

                    b.Navigation("Tags");
                });

            modelBuilder.Entity("UploadImages.Models.Tagmodel", b =>
                {
                    b.Navigation("ImageTagModels");
                });
#pragma warning restore 612, 618
        }
    }
}
