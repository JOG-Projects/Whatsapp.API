using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whatsapp.Domain
{
    public class Text
    {
        [JsonProperty("body")]
        public string Body { get; set; }

        public Text(string body)
        {
            Body = body;
        }
    }
}
