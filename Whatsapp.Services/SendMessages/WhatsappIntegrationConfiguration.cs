using Microsoft.Extensions.Configuration;

namespace Whatsapp.Services.SendMessages
{
    public class WhatsappIntegrationConfiguration
    {
        public WhatsappIntegrationConfiguration(IConfiguration config)
        {
            BaseUrl = $"https://graph.facebook.com/v13.0/{config["PhoneNumberId"]}";
            EndpointPostMessages = $"{BaseUrl}/messages";
            EndpointPostMediaUpload = $"{BaseUrl}/media";
        }

        public string BaseUrl { get; }
        public string EndpointPostMessages { get; }
        public string EndpointPostMediaUpload { get; }
    }
}