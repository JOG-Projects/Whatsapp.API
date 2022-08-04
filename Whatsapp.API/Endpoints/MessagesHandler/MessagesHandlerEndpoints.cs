using Whatsapp.Services;
using Whatsapp.Services.Contracts;
using Whatsapp.Services.HandleMessages;

namespace Whatsapp.API.Endpoints.MessagesHandler
{
    public class MessagesHandlerEndpoints : IEndpointDefinition
    {
        public void DefineEndpoints(WebApplication app)
        {
            app.MapPost("/handleMessage", HandleMessage);
        }

        private IResult HandleMessage(IMessageHandlerServices handlerServices, TextMessageReceived textMessage)
        {
            handlerServices.HandleMessage(textMessage);
            return Results.Ok();
        }
    }
}