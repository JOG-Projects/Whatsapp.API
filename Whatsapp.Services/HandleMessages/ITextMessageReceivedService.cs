namespace Whatsapp.Services.Contracts
{
    public interface ITextMessageReceivedServices
    {
        void HandleMessage(TextMessageReceived receivedMessage);
    }
}