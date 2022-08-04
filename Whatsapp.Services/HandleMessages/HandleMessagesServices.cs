using System.Text.Json;
using Whatsapp.Services.Contracts;
using Whatsapp.Services.RequisitionService;

namespace Whatsapp.Services.HandleMessagesServices
{
    public class HandleMessagesServices : IMessageHandlerServices
    {
        private readonly RequisitionServices _requisitionServices;
        private readonly IMessageServices _messageServices;

        public HandleMessagesServices(RequisitionServices requisitionServices, IMessageServices messageServices)
        {
            _requisitionServices = requisitionServices;
            _messageServices = messageServices;
        }

        public void HandleMessage(MessageReceived receivedMessage)
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
            //switch (change.Value.)
            //{
            //    default:
            //        break;
            //}

            if (change.Value.Messages == null || change.Value.Contacts == null)
            {
                return;
            }

            foreach ((var message, var contact) in change.Value.Messages.Zip(change.Value.Contacts))
            {
                try
                {
                    string receivedMessage = message.Text.Body;
                    string from = contact.Wa_id;

                    await _requisitionServices.HandleMessage(receivedMessage, from);
                }
                catch (Exception ex)
                {
                    await _messageServices.SendTextMessage(new (contact.Wa_id, ex.ToString()));
                }
            }
        }
    }
}