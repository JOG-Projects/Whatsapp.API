using Whatsapp.Domain;

namespace Whatsapp.Services
{
    public class TextMessageReceivedServices : ITextMessageReceivedService
    {
        private readonly IMessageServices MessageServices;

        public TextMessageReceivedServices(IMessageServices messageServices)
        {
            MessageServices = messageServices;
        }

        public async Task HandleMessage(TextMessageReceived receivedMessage)
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

            foreach ((var message, var contact) in change.value.messages.Zip(change.value.contacts))
            {
                var from = contact.wa_id;
                var textMessage = new TextMessage(from, message.text.body + " - recebido");
                await MessageServices.SendMessage(textMessage);
            }
        }
    }
}