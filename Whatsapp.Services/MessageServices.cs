using Newtonsoft.Json;
using System.Net.Http.Headers;
using Whatsapp.Services;
using Whatsapp.Domain;

namespace Whatsapp.Services
{
    public class MessageServices : IMessageServices
    {
        private static HttpClient Client = new();
        private const string Uri = "https://graph.facebook.com/v13.0/106727002107521/messages";

        public MessageServices()
        {
            Configure();
        }

        public async Task<string> SendMessage(TextMessageVM message)
        {
            var txtMessage = new TextMessage(message.To, message.Text, message.PreviewUrl?? true);

            var messageJson = JsonConvert.SerializeObject(txtMessage);

            var x = await Client.PostAsync(Uri, new StringContent(messageJson, System.Text.Encoding.UTF8, "application/json"));

            var stringX = await x.Content.ReadAsStringAsync();

            return stringX;
        }

        private void Configure()
        {
            Client.DefaultRequestHeaders.Authorization = new("Bearer", "EAAvZA54XXes0BAHJ1siACNRLYzNJNqupZCwdFpWw2eHhVZCq21YFTX50ZBlKEMl8ERl2IdyzMXQEyxqEzPZAaTo07UcygbGXGQQw8jP0jQOEdkvH7hBlaeLdkmIafWcinY7sAxFdQkTtbOsljBCiC3c0sfnALUGioPFXqzIuGZCrBcuRk5qDDedZB6ZBqoA7r0t8HjdZApoqEKAZDZD");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}