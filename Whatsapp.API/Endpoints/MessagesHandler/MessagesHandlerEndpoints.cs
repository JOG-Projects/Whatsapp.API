using Whatsapp.Services;
using Whatsapp.Services.Contracts;

namespace Whatsapp.API.Endpoints.MessagesHandler
{
    public class MessagesHandlerEndpoints : IEndpointDefinition
    {
        public void DefineEndpoints(WebApplication app)
        {
            app.MapPost("/handleMessage", HandleMessage);

        }

        private IResult HandleMessage(IMessageHandlerServices textMessageServices, TextMessageReceived textMessage)
        {
            textMessageServices.HandleMessage(textMessage);
            return Results.Ok();
        }
    }
}