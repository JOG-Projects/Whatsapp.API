using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whatsapp.Services;
using Whatsapp.Domain;

namespace Whatsapp.Services
{
    public interface IMessageServices
    {
        public Task<string> SendMessage(TextMessageVM message);

        public Task<string> SendMessage(TextMessage txtMessage);

        public Task<string> SendMediaByUrl(MediaVM media);

        public Task<string> UploadMedia(ImageUploadRequestVM image);

        public string SaveMediaJpg(string mediaBase64);
    }
}
