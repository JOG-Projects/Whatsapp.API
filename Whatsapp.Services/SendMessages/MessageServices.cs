using AutoMapper;
using Microsoft.Extensions.Configuration;
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

        public async Task<string> SendTextMessage(TextMessageVM message)
        {
            var txtMessage = new TextMessage(message.To, message.Text, message.PreviewUrl ?? true);
            return await _httpClient.PostJsonAsync(_whatsappConfiguration.EndpointPostMessages, txtMessage);
        }

        public async Task<string> SendTemplateMessage(TemplateMessageVM templateMessageVM)
        {
            var templateMessage = new TemplateMessage(templateMessageVM.To, templateMessageVM.TemplateName);

            return await _httpClient.PostJsonAsync(_whatsappConfiguration.EndpointPostMessages, templateMessage);
        }

        public async Task<string> SendMediaMessageByUrl<T>(MediaMessageVM mediaVM) where T : MediaMessage
        {
            var media = _mapper.Map<T>(mediaVM);

            return await _httpClient.PostJsonAsync(_whatsappConfiguration.EndpointPostMessages, media);
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