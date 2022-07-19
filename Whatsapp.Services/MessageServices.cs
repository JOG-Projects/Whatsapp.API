using Newtonsoft.Json;
using System.Net.Http.Headers;
using Whatsapp.Services;
using Whatsapp.Domain;
using System.Net.Http.Json;
using System.Text;

namespace Whatsapp.Services
{
    public class MessageServices : IMessageServices
    {
        private static HttpClient Client = new();
        private const string UriText = "https://graph.facebook.com/v13.0/106727002107521/messages";
        private const string UriImage = "https://your-webapp-hostname:your-webapp-port/v1/media";

        public MessageServices()
        {
            Configure();
        }

        public async Task<string> SendMessage(TextMessageVM message)
        {
            var txtMessage = new TextMessage(message.To, message.Text, message.PreviewUrl ?? true);
            return await SendMessage(txtMessage);
        }

        public async Task<string> SendMessage(TextMessage txtMessage)
        {
            var messageJson = JsonConvert.SerializeObject(txtMessage);

            var response = await Client.PostAsync(UriText, new StringContent(messageJson, Encoding.UTF8, "application/json"));

            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }

        public async Task<string> UploadImage(ImageUploader image)
        {
            var imageBytes = Encoding.UTF8.GetBytes(image.Base64);
        
            var response = await Client.PostAsync(UriImage, new ByteArrayContent(imageBytes));
        
            var stringResponse = await response.Content.ReadAsStringAsync();
        
            return stringResponse;
        }

        private void Configure()
        {
            Client.DefaultRequestHeaders.Authorization = new("Bearer", "EAAvZA54XXes0BAP1onahUMp3D4w5YmpeXEhpWWSG3S6kYC0aQLN4G8LGYaoOkBjddTftnk473yxmGVopqs3je7fr9Kn4ImqeOWlBfZBOaa5gEUCoxv2vRFZC6es98TiZCIQ9aFy4aTZA8PpygKmZBOliuFZB4ltFn0lZCqVUVKgTVGwGpqGIcEiQeVdgzg1X2rDwMwR8qcFP9wZDZD");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}