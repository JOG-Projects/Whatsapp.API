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

            app.MapPost("/handleButtonResponse", HandleButtonResponse);
        }

        private IResult HandleButtonResponse(IMessageHandlerServices handlerServices, ButtonResponseReceived buttonResponse)
        {
            handlerServices.HandleButtonResponse(buttonResponse);
            return Results.Ok();
        }

        private IResult HandleMessage(IMessageHandlerServices handlerServices, TextMessageReceived textMessage)
        {
            handlerServices.HandleMessage(textMessage);
            return Results.Ok();
        }
    }
}