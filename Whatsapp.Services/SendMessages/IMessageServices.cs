using Whatsapp.Domain;
using Whatsapp.Services.MediaUpload;
using Whatsapp.Services.ViewModels;

namespace Whatsapp.Services.Contracts
{
    public interface IMessageServices
    {
        public Task<string> SendMessage(TextMessageVM message);

        public Task<string> SendMessage(TextMessage txtMessage);

        public Task<string> SendMediaByUrl(MediaMessageVM media);

        public Task<string> UploadMedia(ImageUploadRequestVM image);
    }
}