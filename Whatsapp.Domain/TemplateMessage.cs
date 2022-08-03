using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whatsapp.Domain
{
    public class TemplateMessage : MessageBase
    {
        public TemplateMessage(string to, string templateName) : base(to)
        {
            Template = new(templateName);
            To = to;
        }

        [JsonProperty("type")]
        public string Type { get; } = "template";

        [JsonProperty("template")]
        public Template Template { get; set; }
    }

    public class Template
    {
        public Template(string name)
        {
            Language = new();
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
        public string Code { get; } = "pt_BR";        
    }
}