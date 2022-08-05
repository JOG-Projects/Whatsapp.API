using Newtonsoft.Json;

namespace Whatsapp.Domain
{
    public class TemplateMessage : MessageBase
    {
        public TemplateMessage(string to, string templateName, List<Component>? componentes = null, string templateLanguage = "pt_BR") : base(to)
        {
            Template = new(templateName, templateLanguage, componentes);
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
        public Component(string type, List<Parameter> parameters, string? subType = null, string? index = null)
        {
            Type = type;
            SubType = subType;
            Index = index;
            Parameters = parameters;
        }

        [JsonProperty("type")] 
        public string Type { get; set; } //"body", "header", "footer", "button"

        [JsonProperty("sub_type")]
        public string? SubType { get; set; } //"body", "header", "footer", "button"

        [JsonProperty("index")]
        public string? Index { get; }

        [JsonProperty("parameters")]
        public List<Parameter> Parameters { get; set; }
    }

    public class TextParameter : Parameter
    {
        public TextParameter(string text) : base("text")
        {
            Text = text;
        }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class ImageParameter : Parameter
    {
        public ImageParameter(string link) : base("image")
        {
            Image = new Image(link);
        }

        [JsonProperty("image")]
        public Image Image { get; }
    }

    public class Image
    {
        [JsonProperty("link")]
        public string Link { get; }

        public Image(string link)
        {
            Link = link;
        }
    }

    public abstract class Parameter
    {
        public Parameter(string type)
        {
            Type = type;
        }

        [JsonProperty("type")]
        public string Type { get; set; } //"text", NAO MAPEADO "currency", NAO MAPEADO "date_time", NAO MAPEADO "image"
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