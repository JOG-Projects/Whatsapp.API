using Newtonsoft.Json;

namespace Whatsapp.Domain
{
    public class TextMessage
    {
        [JsonProperty("messaging_product")]
        public string MessagingProduct { get; } = "whatsapp";

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("type")]
        public string Type { get; } = "text";

        [JsonProperty("text")]
        public Text Text { get; set; }

        [JsonProperty("preview_url")]
        public bool PreviewUrl { get; set; }

        public TextMessage(string to, string text, bool preview = true)
        {
            To = to;
            Text = new(text);
        }
    }
}