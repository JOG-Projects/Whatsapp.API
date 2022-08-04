namespace Whatsapp.Services.Webhook
{
    public interface IWebhookNotifierServices
    {
        public void Add(string endpoint);

        public Task NotifyEndpoints(MessageReceived textMessage);
    }
}