using Whatsapp.Services.Contracts;
using Whatsapp.Services.RequisitionService;

namespace Whatsapp.Services.HandleMessagesServices
{
    public class HandleMessagesServices : IMessageHandlerServices
    {
        private readonly RequisitionServices _requisitionServices;

        public HandleMessagesServices(RequisitionServices requisitionServices)
        {
            _requisitionServices = requisitionServices;
        }

        public void HandleMessage(TextMessageReceived receivedMessage)
        {
            foreach (Entry entry in receivedMessage.Entry)
            {
                foreach (Change change in entry.Changes)
                {
                    Task.Run(() => AnswerMessage(change));
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
                string receivedMessage = message.Text.Body;
                string from = contact.Wa_id;

                await _requisitionServices.HandleMessage(receivedMessage, from);
            }
        }
    }
}