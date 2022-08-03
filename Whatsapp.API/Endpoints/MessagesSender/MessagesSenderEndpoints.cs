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

            app.MapPost("/imageByUrl", SendImageByUrl);

            app.MapPost("/videoByUrl", SendVideoByUrl);

            app.MapPost("/audioByUrl", SendAudioByUrl);

            app.MapPost("/documentByUrl", SendDocumentByUrl);

            app.MapPost("/templateMessage", SendMessageTemplate);
        }

        private static async Task<IResult> SendMessageTemplate(IMessageServices messageServices, TemplateMessageVM templateMessage)
        {
            var result = await messageServices.SendMessageTemplate(templateMessage);
            return Results.Ok(result);
        }

        private static async Task<IResult> SendImageByUrl(IMessageServices messageServices, MediaMessageVM image)
        {
            var result = await messageServices.SendMediaByUrl<ImageMessage>(image);
            return Results.Ok(result);
        }

        private static async Task<IResult> SendVideoByUrl(IMessageServices messageServices, MediaMessageVM video)
        {
            var result = await messageServices.SendMediaByUrl<VideoMessage>(video);
            return Results.Ok(result);
        }

        private static async Task<IResult> SendAudioByUrl(IMessageServices messageServices, MediaMessageVM audio)
        {
            var result = await messageServices.SendMediaByUrl<AudioMessage>(audio);
            return Results.Ok(result);
        }

        private static async Task<IResult> SendDocumentByUrl(IMessageServices messageServices, MediaMessageVM document)
        {
            var result = await messageServices.SendMediaByUrl<DocumentMessage>(document);
            return Results.Ok(result);
        }

        private static async Task<IResult> SendTextMessage(IMessageServices msgServices, TextMessageVM message)
        {
            var result = await msgServices.SendMessage(message);
            return Results.Ok(result);
        }
    }
}