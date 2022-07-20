using Newtonsoft.Json;

namespace Whatsapp.Domain
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
        public string Type { get; private set; } = "image";

        [JsonProperty("image")]
        public MediaImageUrl Image { get; set; }

        public Media(string to, string mediaUrl)
        {
            To = to;
            Image = new MediaImageUrl(mediaUrl);
        }
    }

    public class MediaImageUrl
    {
        public MediaImageUrl(string link)
        {
            Link = link;
        }

        [JsonProperty("link")]
        public string Link { get; set; }
    }
}
