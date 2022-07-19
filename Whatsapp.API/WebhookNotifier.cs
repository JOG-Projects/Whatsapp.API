internal class WebhookNotifier : IWebhookNotifier
{
    private List<string> _list;

    public WebhookNotifier()
    {
        _list = new();
    }

    public void Add(string endpoint)
    {
        _list.Add(endpoint);
    }

    public List<string> GetAll()
    {
        return _list;
    }
}