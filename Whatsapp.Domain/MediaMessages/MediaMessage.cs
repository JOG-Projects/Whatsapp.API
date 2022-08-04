using Newtonsoft.Json;

namespace Whatsapp.Domain.MediaMessages
{
    public class MediaMessage : MessageBase
    {
        [JsonProperty("recipient_type")]
        public string RecipientType { get; private set; } = "individual";

        [JsonProperty("type")]
        public string Type { get; set; }

        public MediaMessage(string to) : base(to)
        {
        }
    }
}