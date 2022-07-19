using Whatsapp.Services;

internal class WebhookNotifier : IWebhookNotifier
{
    private readonly List<string> _list;
    private readonly ITextMessageReceivedService _messageReceivedService;
    private readonly HttpClient _httpClient;

    public WebhookNotifier(ITextMessageReceivedService messageReceivedService, HttpClient httpClient)
    {
        _list = new();
        _messageReceivedService = messageReceivedService;
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
            await _httpClient.PostAsync(endpoint, JsonContent.Create(textMessage));
        }));
    }
}