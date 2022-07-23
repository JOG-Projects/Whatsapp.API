namespace Whatsapp.Services.Webhook
{
    public interface IWebhookNotifierServices
    {
        void Add(string endpoint);

        public Task<List<(string, string)>> NotifyEndpoints(object textMessage);
    }
}