using Newtonsoft.Json;
namespace Whatsapp.Domain.MediaMessages
{
    public class VideoMessage : MediaMessage
    {
        [JsonProperty("video")]
        public MediaUrl MediaType { get; set; }

        public VideoMessage(string to, string mediaUrl) : base(to)
        {
            Type = "video";
            To = to;
            MediaType = new(mediaUrl);
        }
    }
}