using Newtonsoft.Json;

namespace Whatsapp.Domain.MediaMessages
{
    public partial class ImageMessage : MediaMessage
    {
        [JsonProperty("image")]
        public MediaUrl MediaType { get; set; }

        public ImageMessage(string to, string mediaUrl) : base(to)
        {
            Type = "image";
            To = to;
            MediaType = new(mediaUrl);
        }
    }
}