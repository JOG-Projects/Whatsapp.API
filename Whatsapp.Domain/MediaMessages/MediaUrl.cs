using Newtonsoft.Json;

namespace Whatsapp.Domain.MediaMessages
{
    public class MediaUrl
    {
        public MediaUrl(string link)
        {
            Link = link;
        }

        [JsonProperty("link")]
        public string Link { get; set; }
    }
}