using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UploadImages.Migrations
{
    /// <inheritdoc />
    public partial class newrelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tags",
                table: "ImageModels");

            migrationBuilder.CreateTable(
                name: "TagModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagModels_ImageModels_ImageModelId",
                        column: x => x.ImageModelId,
                        principalTable: "ImageModels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ImageTags",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageTags", x => new { x.ImageId, x.TagId });
                    table.ForeignKey(
                        name: "FK_ImageTags_ImageModels_ImageId",
                        column: x => x.ImageId,
                        principalTable: "ImageModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImageTags_TagModels_TagId",
                        column: x => x.TagId,
                        principalTable: "TagModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImageTags_TagId",
                table: "ImageTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_TagModels_ImageModelId",
                table: "TagModels",
                column: "ImageModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageTags");

            migrationBuilder.DropTable(
                name: "TagModels");

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "ImageModels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
