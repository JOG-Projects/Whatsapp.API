internal interface IWebhookNotifier
{
    void Add(string endpoint);

    List<string> GetAll();
}