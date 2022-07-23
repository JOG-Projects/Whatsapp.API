using Newtonsoft.Json;

namespace Whatsapp.Domain.MediaMessages
{
    public class MediaMessage
    {
        [JsonProperty("messaging_product")]
        public string MessagingProduct { get; private set; } = "whatsapp";

        [JsonProperty("recipient_type")]
        public string RecipientType { get; private set; } = "individual";

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        public MediaMessage(string to)
        {
            To = to;
        }
    }
}