using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whatsapp.Domain.Media
{
    public class Video : Media
    {
        [JsonProperty("video")]
        public MediaUrl MediaType { get; set; }

        public Video(string to, string mediaUrl) : base(to)
        {
            Type = "video";
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
