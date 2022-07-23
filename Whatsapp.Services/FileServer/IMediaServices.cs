using Whatsapp.Services.MediaUpload;

namespace Whatsapp.Services.FileServer
{
    public interface IMediaServices
    {
        public string SaveImage(ImageUploadRequestVM image);
    }
}