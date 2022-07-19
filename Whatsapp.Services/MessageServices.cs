using Newtonsoft.Json;
using System.Net.Http.Headers;
using Whatsapp.Domain;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Whatsapp.Services
{
    public class MessageServices : IMessageServices
    {
        private readonly HttpClient _httpClient;
        private string BaseUrl { get; }
        private string EndpointPostMessages { get; }
        private string EndpointPostMediaUpload { get; }

        public MessageServices(IConfiguration configuration, HttpClient httpClient)
        {
            BaseUrl = $"https://graph.facebook.com/v13.0/{configuration["PhoneNumberId"]}";
            EndpointPostMessages = $"{BaseUrl}/messages";
            EndpointPostMediaUpload = $"{BaseUrl}/media";

            _httpClient = httpClient;
            ConfigureHttpClient(configuration["Bearer"]);
        }

        public async Task<string> SendMessage(TextMessageVM message)
        {
            var txtMessage = new TextMessage(message.To, message.Text, message.PreviewUrl ?? true);
            return await SendMessage(txtMessage);
        }

        public async Task<string> SendMessage(TextMessage txtMessage)
        {
            var messageJson = JsonConvert.SerializeObject(txtMessage);

            var response = await _httpClient.PostAsync(EndpointPostMessages, new StringContent(messageJson, Encoding.UTF8, "application/json"));

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

            var response = await _httpClient.PostAsync(EndpointPostMediaUpload, content);

            var stringResponse = await response.Content.ReadAsStringAsync();

            File.Delete(path);

            return stringResponse;
        }

        private void ConfigureHttpClient(string bearer)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", bearer);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}