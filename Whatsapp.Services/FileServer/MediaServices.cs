using Microsoft.Extensions.Hosting;
using Whatsapp.Services.FileServer;
using Whatsapp.Services.MediaUpload;

namespace Whatsapp.Services
{
    public class MediaServices : IMediaServices
    {
        private string FilesDirectory { get; }

        public MediaServices(IHostEnvironment env)
        {
            FilesDirectory = Path.Combine(env.ContentRootPath, "images");
            Directory.CreateDirectory(FilesDirectory);
        }

        public string SaveImage(ImageUploadRequestVM imageRequest)
        {
            var media = Convert.FromBase64String(imageRequest.Base64);

            var fileName = $"{Guid.NewGuid()}{imageRequest.Type}";

            File.WriteAllBytes(Path.Combine(FilesDirectory, fileName), media);

            return fileName;
        }
    }
}
