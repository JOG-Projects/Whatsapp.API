using Whatsapp.Services;

internal interface IWebhookNotifier
{
    void Add(string endpoint);

    Task NotifyEndpoints(TextMessageReceived textMessage);
}