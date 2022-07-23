using Whatsapp.Services;
using Whatsapp.Services.Contracts;
using Whatsapp.Services.FileServer;
using Whatsapp.Services.HandleMessagesServices;
using Whatsapp.Services.SendMessages;
using Whatsapp.Services.Webhook;

namespace Whatsapp.API
{
    public static class DependencyInjectionRegistration
    {
        public static void AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddSingleton<HttpClient>();
            services.AddSingleton<IMessageServices, MessageServices>();
            services.AddSingleton<IWebhookNotifierServices, WebhookNotifierServices>();
            services.AddSingleton<ITextMessageReceivedServices, HandleMessagesServices>();
            services.AddSingleton<IMediaServices, MediaServices>();
        }
    }
}