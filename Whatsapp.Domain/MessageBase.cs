using Newtonsoft.Json;

namespace Whatsapp.Domain
{
    public class MessageBase
    {
        [JsonProperty("messaging_product")]
        public string MessagingProduct { get; } = "whatsapp";

        [JsonProperty("to")]
        public string To { get; set; }

        public MessageBase(string to)
        {
            To = to;
        }
    }
}