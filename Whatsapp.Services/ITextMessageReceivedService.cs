namespace Whatsapp.Services
{
    public interface ITextMessageReceivedService
    {
        Task HandleMessage(TextMessageReceived receivedMessage);
    }
}