using System.Net.Http.Json;
using Whatsapp.Services.Contracts;

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

        public async Task NotifyEndpoints(TextMessageReceived textMessage)
        {
            if (_client == string.Empty)
            {
                _messageHandlerServices.HandleMessage(textMessage);
                return;
            }

            await GetReturn(_client, textMessage);
        }

        private async Task GetReturn(string endpoint, TextMessageReceived textMessage)
        {
            await _httpClient.PostAsync(endpoint, JsonContent.Create(textMessage));
        }
    }
}