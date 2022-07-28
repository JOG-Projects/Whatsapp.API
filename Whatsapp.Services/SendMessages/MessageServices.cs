using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Whatsapp.Domain;
using Whatsapp.Domain.MediaMessages;
using Whatsapp.Services.Contracts;
using Whatsapp.Services.MediaUpload;
using Whatsapp.Services.ViewModels;

namespace Whatsapp.Services.SendMessages
{
    public class MessageServices : IMessageServices
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        private string BaseUrl { get; }
        private string EndpointPostMessages { get; }
        private string EndpointPostMediaUpload { get; }


        public MessageServices(IConfiguration configuration, HttpClient httpClient, IHostEnvironment environment, IMapper mapper)
        {
            BaseUrl = $"https://graph.facebook.com/v13.0/{configuration["PhoneNumberId"]}";
            EndpointPostMessages = $"{BaseUrl}/messages";
            EndpointPostMediaUpload = $"{BaseUrl}/media";

            _httpClient = httpClient;
            _mapper = mapper;
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

        public async Task<string> SendMediaByUrl<T>(MediaMessageVM mediaVM) where T : MediaMessage
        {
            var media = _mapper.Map<T>(mediaVM);

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

        public async Task<string> SendDefaultMessage(string from)
        {
            var textMessage = new TextMessageVM(from, "teste");
            return await SendMessage(textMessage);
        }
    }
}