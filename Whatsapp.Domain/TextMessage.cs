using Newtonsoft.Json;

namespace Whatsapp.Domain
{
    public class TextMessage : MessageBase
    {
        [JsonProperty("type")]
        public string Type { get; } = "text";

        [JsonProperty("text")]
        public Text Text { get; set; }

        [JsonProperty("preview_url")]
        public bool PreviewUrl { get; set; }

        public TextMessage(string to, string text, bool preview = true): base(to)
        {            
            Text = new(text);
        }
    }
}