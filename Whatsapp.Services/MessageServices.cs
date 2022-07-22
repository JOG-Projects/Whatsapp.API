using Newtonsoft.Json;
using System.Net.Http.Headers;
using Whatsapp.Domain;
using System.Text;
using Microsoft.Extensions.Configuration;
using Whatsapp.Domain.Media;
using Microsoft.Extensions.Hosting;

namespace Whatsapp.Services
{
    public class MessageServices : IMessageServices
    {
        private readonly HttpClient _httpClient;
        private string BaseUrl { get; }
        private string EndpointPostMessages { get; }
        private string EndpointPostMediaUpload { get; }

        private string FilesDirectory;

        public MessageServices(IConfiguration configuration, HttpClient httpClient, IHostEnvironment environment)
        {
            BaseUrl = $"https://graph.facebook.com/v13.0/{configuration["PhoneNumberId"]}";
            EndpointPostMessages = $"{BaseUrl}/messages";
            EndpointPostMediaUpload = $"{BaseUrl}/media";

            _httpClient = httpClient;
            ConfigureHttpClient(configuration["Bearer"]);

            FilesDirectory = Path.Combine(environment.ContentRootPath, "StaticFiles");

            Directory.CreateDirectory(FilesDirectory);
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

        public string SaveMediaJpg(string mediaBase64)
        {
            var media = Convert.FromBase64String(mediaBase64);

            var fileName = Guid.NewGuid().ToString() + ".jpg";

            File.WriteAllBytes(Path.Combine(FilesDirectory, fileName), media);

            return fileName;
        }

        public async Task<string> SendMediaByUrl(MediaVM mediaVM)
        {
            var media = new Image(mediaVM.To, mediaVM.Link);

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