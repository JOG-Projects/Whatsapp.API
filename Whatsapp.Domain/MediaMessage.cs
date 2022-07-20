using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whatsapp.Domain
{

    public class MediaMessage: MessageBase
    {
        public string recipient_type { get; set; }
        public string type { get; } = "image";
        public Media image { get; set; }

        public MediaMessage(string to, string image) : base(to)
        {
        }
    }
}