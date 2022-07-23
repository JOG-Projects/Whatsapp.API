using Whatsapp.Domain;
using Whatsapp.Services.Contracts;

namespace Whatsapp.Services.HandleMessagesServices
{
    public class HandleMessagesServices : ITextMessageReceivedServices
    {
        private readonly IMessageServices MessageServices;

        public HandleMessagesServices(IMessageServices messageServices)
        {
            MessageServices = messageServices;
        }

        public async Task HandleMessage(TextMessageReceived receivedMessage)
        {
            foreach (Entry entry in receivedMessage.Entry)
            {
                foreach (Change change in entry.Changes)
                {
                    await AnswerMessage(change);
                }
            }
        }

        private async Task AnswerMessage(Change change)
        {
            if (change.Value.Messages == null || change.Value.Contacts == null)
            {
                return;
            }

            foreach ((var message, var contact) in change.Value.Messages.Zip(change.Value.Contacts))
            {
                var from = contact.Wa_id;
                var textMessage = new TextMessage(from, $"{message.Text.Body} - recebido");
                await MessageServices.SendMessage(textMessage);
            }
        }
    }
}