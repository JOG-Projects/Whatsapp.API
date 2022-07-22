using Newtonsoft.Json;

namespace Whatsapp.Domain.Media
{
    public class Media
    {
        [JsonProperty("messaging_product")]
        public string MessagingProduct { get; private set; } = "whatsapp";

        [JsonProperty("recipient_type")]
        public string RecipientType { get; private set; } = "individual";

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }  

        public Media(string to)
        {
            To = to;
        }
    }    
}