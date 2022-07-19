using Newtonsoft.Json;
using System.Net.Http.Headers;
using Whatsapp.Services;
using Whatsapp.Domain;
using System.Net.Http.Json;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Whatsapp.Services
{
    public class MessageServices : IMessageServices
    {
        private string Number { get; }
        private static HttpClient Client = new();
        private readonly HttpClient _httpClient;
        private string BaseUrl { get; }
        private string EndpointPostMessages { get; }
        private string EndpointPostMediaUpload { get; }

        public MessageServices(IConfiguration configuration, middlewareWebhook)
        {
            Number = configuration["phoneNumber"];
            BaseUrl = $"https://graph.facebook.com/v13.0/{Number}";
            EndpointPostMessages = $"{BaseUrl}/messages";
            EndpointPostMediaUpload = $"{BaseUrl}/media";

            _httpClient = httpClient;
            ConfigureHttpClient();
        }

        public async Task<string> SendMessage(TextMessageVM message)
        {
            var txtMessage = new TextMessage(message.To, message.Text, message.PreviewUrl ?? true);
            return await SendMessage(txtMessage);
        }

        public async Task<string> SendMessage(TextMessage txtMessage)
        {
            var messageJson = JsonConvert.SerializeObject(txtMessage);

            var response = await Client.PostAsync(EndpointPostMessages, new StringContent(messageJson, Encoding.UTF8, "application/json"));

            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }

        //public async Task<string> UploadImage(ImageUploader image)
        //{
        //    var imageBytes = Encoding.UTF8.GetBytes(image.Base64);
        //
        //    var response = await Client.PostAsync(UriImage, new ByteArrayContent(imageBytes));
        //
        //    var stringResponse = await response.Content.ReadAsStringAsync();
        //
        //    return stringResponse;
        //}

        public async Task<string> UploadMedia(ImageUploader image)
        {
            MultipartFormDataContent content = new();

            var imageBytes = Encoding.UTF8.GetBytes(image.Base64);
            var path = new Guid().ToString() + ".jpg";
            File.WriteAllBytes(path, imageBytes);

            content.Add(new StringContent($"file=@{path};type=image/jpeg;"));
            content.Add(new StringContent("messaging_product=whatsapp;"));

            var response = await Client.PostAsync(EndpointPostMediaUpload, content);

            var stringResponse = await response.Content.ReadAsStringAsync();

            File.Delete(path);

            return stringResponse;
        }

        private void ConfigureHttpClient()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", "EAAvZA54XXes0BAP1onahUMp3D4w5YmpeXEhpWWSG3S6kYC0aQLN4G8LGYaoOkBjddTftnk473yxmGVopqs3je7fr9Kn4ImqeOWlBfZBOaa5gEUCoxv2vRFZC6es98TiZCIQ9aFy4aTZA8PpygKmZBOliuFZB4ltFn0lZCqVUVKgTVGwGpqGIcEiQeVdgzg1X2rDwMwR8qcFP9wZDZD");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}