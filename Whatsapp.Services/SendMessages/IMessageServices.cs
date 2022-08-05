using Whatsapp.Domain;
using Whatsapp.Domain.MediaMessages;
using Whatsapp.Services.MediaUpload;
using Whatsapp.Services.ViewModels;

namespace Whatsapp.Services.Contracts
{
    public interface IMessageServices
    {
        public Task<SuccessResponse> SendTextMessage(TextMessageVM message);

        public Task<SuccessResponse> SendMediaMessageByUrl<T>(MediaMessageVM mediaVM) where T : MediaMessage;

        public Task<SuccessResponse> UploadMedia(ImageUploadRequestVM image);

        public Task<SuccessResponse> SendTemplateMessage(TemplateMessageVM templateMessageVM);
    }
}