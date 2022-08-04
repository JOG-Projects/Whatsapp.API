using Whatsapp.Services.Contracts;
using Whatsapp.Services.Extensions;

namespace Whatsapp.Services.Webhook
{
    public class WebhookNotifierServices : IWebhookNotifierServices
    {
        private string _client;
        private readonly HttpClient _httpClient;
        private readonly IMessageHandlerServices _messageHandlerServices;

        public WebhookNotifierServices(IMessageHandlerServices messageHandlerServices, HttpClient httpClient)
        {
            _client = string.Empty;
            _messageHandlerServices = messageHandlerServices;
            _httpClient = httpClient;
        }

        public void Add(string endpoint)
        {
            _client = endpoint;
        }

        public async Task NotifyEndpoints(MessageReceived textMessage)
        {
            if (_client == string.Empty)
            {
                _messageHandlerServices.HandleMessage(textMessage);
                return;
            }

            await _httpClient.PostJsonAsync(_client, textMessage);
        }
    }
}