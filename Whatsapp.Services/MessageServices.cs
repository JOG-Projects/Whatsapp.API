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

        //public async Task<string> SendMessage(TextMessageVM message)
        //{
        //    var txtMessage = new TextMessage(message.To, message.Text, message.PreviewUrl ?? true);
        //
        //    var messageJson = JsonConvert.SerializeObject(txtMessage);
        //
        //    var response = await Client.PostAsync(UriText, new StringContent(messageJson, Encoding.UTF8, "application/json"));
        //
        //    var responseString = await response.Content.ReadAsStringAsync();
        //
        //    return responseString;
        //}

        public async Task<string> UploadImage(ImageUploader image)
        {
            var imageBytes = Encoding.UTF8.GetBytes(image.Base64);
        
            var response = await Client.PostAsync(UriImage, new ByteArrayContent(imageBytes));
        
            var stringResponse = await response.Content.ReadAsStringAsync();
        
            return stringResponse;
        }

        private void Configure()
        {
            Client.DefaultRequestHeaders.Authorization = new("Bearer", "EAAvZA54XXes0BABSFqeOeMxM9CmWcT9n1i3dMDNqVZBGIc6W56ZCxc2vCYk7fxGPZBQPVi4ZAwTgZB041QE14NlfvtZAgnBwIZC7G5xpUXzGfiZCoMZCXX2qnOTVwYar8N8vWboIGR82TIuFlLfguBMIn8oeGBD5rgaSmowdYReMx7vSGYzw0X2EdZAzJwR1HZCZBfi61qzmmzRSB8QZDZD");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}