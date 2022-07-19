namespace Whatsapp.Services
{
    public interface ITextMessageReceivedServices
    {
        Task GetMessage(TextMessageReceived receivedMessage);
    }
}