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
        public string to { get; set; }
        public string type { get; set; }
        public Image image { get; set; }

        public MediaMessage(string to) : base(to)
        {
        }
    }
}