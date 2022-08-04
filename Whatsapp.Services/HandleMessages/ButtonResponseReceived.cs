using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whatsapp.Services.HandleMessages
{
    public class Action
    {
        public List<Button> Buttons { get; set; }
    }

    public class Body
    {
        public string Text { get; set; }
    }

    public class Button
    {
        public string Type { get; set; }
        public Reply Reply { get; set; }
    }

    public class Interactive
    {
        public string Type { get; set; }
        public Body Body { get; set; }
        public Action Action { get; set; }
    }

    public class Reply
    {
        public string Id { get; set; }
        public string Title { get; set; }
    }

    public class ButtonResponseReceived
    {
        public string Messaging_product { get; set; }
        public string Recipient_type { get; set; }
        public string To { get; set; }
        public string Type { get; set; }
        public Interactive Interactive { get; set; }
    }
}
