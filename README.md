# Upload Images

A personal ASP.NET Core MVC application for uploading, browsing, organizing, and managing images. Images can be uploaded in batches, described with text, and organized using tags. The application also supports searching for images by tag.

> Personal project built in 2024.

## Features

- Upload multiple images in one submission
- Add a description and comma-separated tags to uploaded images
- Browse uploaded images in a gallery
- Open an image's detail page to view its description, tags, and upload date
- Search for images using one or more tags
- Get tag suggestions while searching
- Edit an image's description and tags
- Delete an image and its stored file

## Built With

- C#
- ASP.NET Core MVC
- .NET 8
- Entity Framework Core 9
- Microsoft SQL Server
- Razor Views
- Bootstrap
- jQuery
- Select2

## How It Works

The application follows the ASP.NET Core MVC pattern:

- **Controllers** handle incoming requests and select the appropriate views.
- **Services** contain the image and tag operations.
- **Entity Framework Core** handles database access.
- **Models** represent images, tags, and the many-to-many relationship between them.
- **Razor Views** provide the user interface.

Uploaded image files are stored in `wwwroot/images`, while image information and tag relationships are stored in SQL Server.

## Getting Started

### Prerequisites

Make sure you have the following installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/sql-server) or SQL Server LocalDB
- An IDE such as [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

### 1. Clone the repository

```bash
git clone https://github.com/TacoKrydda/UploadImagesApp.git
cd UploadImagesApp/UploadImages
```

### 2. Configure the database

The application reads its SQL Server connection string from `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=UploadImagesDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

The repository is configured to use SQL Server LocalDB by default. If you use another SQL Server instance, update `DefaultConnection` to match your environment.

### 3. Apply database migrations

From the `UploadImages` project directory, run:

```bash
dotnet ef database update
```

If the Entity Framework command-line tool is not installed, install it with:

```bash
dotnet tool install --global dotnet-ef
```

### 4. Run the application

```bash
dotnet restore
dotnet run
```

Open the local URL printed in the terminal. The default route takes you to the uploaded-images page.

## Project Structure

```text
UploadImages/
├── Context/
│   └── UploadImagesContext.cs
├── Controllers/
│   └── HomeController.cs
├── Migrations/
├── Models/
│   ├── ImageModel.cs
│   ├── ImageTagModel.cs
│   └── Tagmodel.cs
├── Services/
│   ├── IImageService.cs
│   └── ImageService.cs
├── Views/
│   └── Home/
├── wwwroot/
│   └── images/
├── appsettings.json
├── Program.cs
└── UploadImages.csproj
```

## Notes

- The default database connection uses SQL Server LocalDB, which is commonly available with Visual Studio on Windows.
- Uploaded files are stored locally in the application's `wwwroot/images` directory.
- This project was created as a personal learning project in 2024.

## Author

[TacoKrydda](https://github.com/TacoKrydda)
