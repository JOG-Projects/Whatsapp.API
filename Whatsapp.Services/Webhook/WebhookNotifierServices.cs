using System.Net.Http.Json;

namespace Whatsapp.Services.Webhook
{
    public class WebhookNotifierServices : IWebhookNotifierServices
    {
        private readonly List<string> _clients;
        private readonly HttpClient _httpClient;

        public WebhookNotifierServices(HttpClient httpClient)
        {
            _clients = new();
            _httpClient = httpClient;
        }

        public void Add(string endpoint)
        {
            _clients.Add(endpoint);
        }

        public async Task<List<(string, string)>> NotifyEndpoints(TextMessageReceived textMessage)
        {
            var tasks = _clients.Select(c => GetReturn(c, textMessage));

            var results = await Task.WhenAll(tasks);

            return results.ToList();
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