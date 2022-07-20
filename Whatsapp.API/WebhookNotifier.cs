internal class WebhookNotifier : IWebhookNotifier
{
    private readonly List<string> _list;
    private readonly HttpClient _httpClient;

    public WebhookNotifier(HttpClient httpClient)
    {
        _list = new();
        _httpClient = httpClient;
    }

    public void Add(string endpoint)
    {
        _list.Add(endpoint);
    }
    public async Task NotifyEndpoints(object textMessage)
    {
        await Task.Run(() => _list.ForEach(async (endpoint) =>
        {
            try
            {
                await _httpClient.PostAsync(endpoint, JsonContent.Create(textMessage));
            }
            catch { }
        }));
    }
}