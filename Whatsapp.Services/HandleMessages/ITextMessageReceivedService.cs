namespace Whatsapp.Services.Contracts
{
    public interface ITextMessageReceivedServices
    {
        Task HandleMessage(TextMessageReceived receivedMessage);
    }
}