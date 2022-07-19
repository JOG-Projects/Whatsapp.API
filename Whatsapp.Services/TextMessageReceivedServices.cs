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
                    await AnswerMessage(change);
                }
            }
        }

        private async Task AnswerMessage(Change change)
        {
            if (change.value.messages == null || change.value.contacts == null)
            {
                return;
            }

            foreach (Message message in change.value.messages)
            {
                var from = change.value.contacts[0].wa_id;
                var textMessage = new TextMessage(from, message.text.body + " - recebido");
                await MessageServices.SendMessage(textMessage);
            }
        }
    }
}