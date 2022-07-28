﻿using Whatsapp.Domain;
using Whatsapp.Domain.MediaMessages;
using Whatsapp.Services.MediaUpload;
using Whatsapp.Services.ViewModels;

namespace Whatsapp.Services.Contracts
{
    public interface IMessageServices
    {
        public Task<string> SendMessage(TextMessageVM message);

        public Task<string> SendMessage(TextMessage txtMessage);

        public Task<string> SendMediaByUrl<T>(MediaMessageVM mediaVM) where T : MediaMessage;

        public Task<string> UploadMedia(ImageUploadRequestVM image);

        public Task<string> SendDefaultMessage(string from);
    }
}