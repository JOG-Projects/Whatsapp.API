using Whatsapp.Domain.MediaMessages;
using Whatsapp.Services.Contracts;
using Whatsapp.Services.ViewModels;

namespace Whatsapp.API.Endpoints.MessagesSender
{
    public class MessagesSenderEndpoints : IEndpointDefinition
    {
        public void DefineEndpoints(WebApplication app)
        {
            app.MapPost("/textMessage", SendTextMessage);

            app.MapPost("/MediaByUrl", SendMedia);
        }

        private static async Task<IResult> SendMedia(IMessageServices messageServices, MediaMessageVM image)
        {
            var result = await messageServices.SendMediaByUrl<VideoMessage>(image);
            return Results.Ok(result);
        }

        private static async Task<IResult> SendTextMessage(IMessageServices msgServices, TextMessageVM message)
        {
            var result = await msgServices.SendMessage(message);
            return Results.Ok(result);
        }
    }
}