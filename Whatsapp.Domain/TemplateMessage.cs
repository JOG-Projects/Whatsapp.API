using Newtonsoft.Json;

namespace Whatsapp.Domain
{
    public class TemplateMessage : MessageBase
    {
        public TemplateMessage(string to, string templateName, string templateLanguage = "pt_BR") : base(to)
        {
            Template = new(templateName, templateLanguage);
        }

        [JsonProperty("type")]
        public string Type { get; } = "template";

        [JsonProperty("template")]
        public Template Template { get; set; }
    }

    public class Template
    {
        public Template(string name, string templateLanguage)
        {
            Language = new(templateLanguage);
            Name = name;
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("language")]
        public Language Language { get; set; }
    }

    public class Language
    {
        [JsonProperty("code")]
        public string Code { get; }

        public Language(string code)
        {
            Code = code;
        }
    }
}