namespace Whatsapp.Services.Webhook
{
    public interface IWebhookNotifierServices
    {
        void Add(string endpoint);

        public Task<(string, string)> NotifyEndpoints(TextMessageReceived textMessage);
    }
}