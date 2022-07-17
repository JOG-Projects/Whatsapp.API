using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whatsapp.Domain;

namespace Whatsapp.Services
{
    public class TextMessageReceivedServices : ITextMessageReceivedServices
    {
        private readonly IMessageServices MessageServices;

        public TextMessageReceivedServices(IMessageServices messageServices)
        {
            MessageServices = messageServices;
        }

        public async Task GetMessage(TextMessageReceived receivedMessage)
        {
            foreach (Entry entry in receivedMessage.entry)
            {
                foreach (Change change in entry.changes)
                {
                    foreach (Message message in change.value.messages)
                    {
                        var textMessage = new TextMessage(change.value.contacts[0].wa_id, message.text.body + " - recebido");
                        await MessageServices.SendMessage(textMessage);
                    }
                }
            }
        }
    }
}