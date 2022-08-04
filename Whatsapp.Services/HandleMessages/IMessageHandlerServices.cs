using Whatsapp.Services.HandleMessages;

namespace Whatsapp.Services.Contracts
{
    public interface IMessageHandlerServices
    {
        void HandleMessage(TextMessageReceived receivedMessage);
    }
}