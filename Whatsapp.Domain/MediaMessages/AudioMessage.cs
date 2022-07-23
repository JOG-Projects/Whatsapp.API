using Newtonsoft.Json;

namespace Whatsapp.Domain.MediaMessages
{
    public class AudioMessage : MediaMessage
    {
        [JsonProperty("audio")]
        public MediaUrl MediaType { get; set; }

        public AudioMessage(string to, string mediaUrl) : base(to)
        {
            Type = "image";
            To = to;
            MediaType = new(mediaUrl);
        }
    }
}