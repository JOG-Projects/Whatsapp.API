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

        private async Task<IResult> HandleMessage(ITextMessageReceivedServices textMessageServices, TextMessageReceived textMessage)
        {
            await textMessageServices.HandleMessage(textMessage);
            return Results.Ok();
        }
    }
}