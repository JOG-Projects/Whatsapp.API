using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Whatsapp.Domain;
using Whatsapp.Domain.MediaMessages;
using Whatsapp.Services.Contracts;
using Whatsapp.Services.Extensions;
using Whatsapp.Services.MediaUpload;
using Whatsapp.Services.ViewModels;

namespace Whatsapp.Services.SendMessages
{
    public class MessageServices : IMessageServices
    {
        private readonly WhatsappIntegrationConfiguration _whatsappConfiguration;
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public MessageServices(IConfiguration configuration, IMapper mapper, WhatsappIntegrationConfiguration whatsappConfiguration, HttpClient httpClient)
        {
            _whatsappConfiguration = whatsappConfiguration;

            _httpClient = httpClient;
            _mapper = mapper;
            ConfigureHttpClient(configuration["Bearer"]);
        }

        public async Task<SuccessResponse> SendMessage<T>(object viewModel)
        {
            var message = _mapper.Map<T>(viewModel)!;
            var jsonResponse = await _httpClient.PostJsonAsync(_whatsappConfiguration.EndpointPostMessages, message);
            return JsonExtensions.TryJsonDeserialize<SuccessResponse>(jsonResponse) ?? throw new Exception(jsonResponse);
        }

        public async Task<SuccessResponse> SendTextMessage(TextMessageVM model)
        {
            return await SendMessage<TextMessage>(model);
        }

        public async Task<SuccessResponse> SendTemplateMessage(TemplateMessageVM model)
        {
            return await SendMessage<TemplateMessage>(model);
        }

        public async Task<SuccessResponse> SendMediaMessageByUrl<T>(MediaMessageVM mediaVM) where T : MediaMessage
        {
            return await SendMessage<T>(mediaVM);
        }

        public Task<string> UploadMedia(ImageUploadRequestVM image)
        {
            throw new NotImplementedException();
        }

        private void ConfigureHttpClient(string bearer)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", bearer);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}