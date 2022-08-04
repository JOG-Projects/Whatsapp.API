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
        public Template(string name, string templateLanguage, List<Component>? componentes = null)
        {
            Components = componentes;
            Language = new(templateLanguage);
            Name = name;
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("language")]
        public Language Language { get; set; }

        [JsonProperty("components")]
        public List<Component>? Components { get; set; }

    }
    public class Component
    {
        public Component(string type, List<Parameter> parameters)
        {
            Type = type;
            Parameters = parameters;
        }

        [JsonProperty("type")] 
        public string Type { get; set; } //"body", "header", "footer"

        [JsonProperty("parameters")]
        public List<Parameter> Parameters { get; set; }
    }

    public class Parameter
    {
        public Parameter(string type, string text)
        {
            Type = type;
            Text = text;
        }

        [JsonProperty("type")]
        public string Type { get; set; } //"text", NAO MAPEADO "currency", NAO MAPEADO "date_time", NAO MAPEADO "image"

        [JsonProperty("text")]
        public string Text { get; set; }
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