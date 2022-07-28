using Newtonsoft.Json;

namespace Whatsapp.Domain.MediaMessages
{
    public class DocumentMessage : MediaMessage
    {
        [JsonProperty("document")]
        public MediaUrl MediaType { get; set; }

        public DocumentMessage(string to, string mediaUrl) : base(to)
        {
            Type = "document";
            To = to;
            MediaType = new(mediaUrl);
        }
    }
}