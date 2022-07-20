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

        public async Task<string> SendMediaByUrl(MediaVM mediaVM)
        {
            var media = new Media(mediaVM.To, mediaVM.Link);

            var response = await _httpClient.PostAsync(EndpointPostMessages, new StringContent(JsonConvert.SerializeObject(media), Encoding.UTF8, "application/json"));

            var stringResponse = await response.Content.ReadAsStringAsync();

            return stringResponse;
        }

        private void ConfigureHttpClient(string bearer)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", bearer);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public Task<string> UploadMedia(ImageUploadRequestVM image)
        {
            throw new NotImplementedException();
        }
    }
}