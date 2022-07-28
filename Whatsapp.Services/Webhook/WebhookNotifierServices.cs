using System.Net.Http.Json;

namespace Whatsapp.Services.Webhook
{
    public class WebhookNotifierServices : IWebhookNotifierServices
    {
        private string _client;
        private readonly HttpClient _httpClient;

        public WebhookNotifierServices(HttpClient httpClient)
        {
            _client = "";
            _httpClient = httpClient;
        }

        public void Add(string endpoint)
        {
            _client = endpoint;
        }

        public async Task<(string, string)> NotifyEndpoints(TextMessageReceived textMessage)
        {
            return await GetReturn(_client, textMessage);
        }

        private async Task<(string endpoint, string)> GetReturn(string endpoint, TextMessageReceived textMessage)
        {
            try
            {
                var response = await _httpClient.PostAsync(endpoint, JsonContent.Create(textMessage));
                var res = await response.Content.ReadAsStringAsync();

                return (endpoint, res);
            }
            catch
            {
                return (endpoint, "Error");
            }
        }
    }
}