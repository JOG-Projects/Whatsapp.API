using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whatsapp.Domain.Media
{
    public class Audio : Media
    {
        [JsonProperty("audio")]
        public MediaUrl MediaType { get; set; }

        public Audio(string to, string mediaUrl) : base(to)
        {
            Type = "image";
            To = to;
            MediaType = new(mediaUrl);
        }

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
}
