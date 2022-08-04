using Microsoft.AspNetCore.Mvc;
using Whatsapp.Domain;
using Whatsapp.Services;
using Whatsapp.Services.Webhook;

namespace Whatsapp.API.Endpoints.WebhookRedirect
{
    public class WebhookRedirectEndpoints : IEndpointDefinition
    {
        public void DefineEndpoints(WebApplication app)
        {
            app.MapPost("/webhook", NotifierHandler);

            app.MapGet("/webhook", VerifyWebhook);

            app.MapGet("/subscribeWebhook", Subscribe);
        }

        private static IResult Subscribe(IWebhookNotifierServices notifier, string endpoint)
        {
            notifier.Add(endpoint);
            return Results.Ok(endpoint);
        }

        private static IResult VerifyWebhook(IConfiguration configuration,
            [FromQuery(Name = "hub.mode")] string hubMode,
            [FromQuery(Name = "hub.challenge")] int hubChallenge,
            [FromQuery(Name = "hub.verify_token")] string hubVerifyToken)
        {
            if (hubVerifyToken == configuration["VerifyToken"] && hubMode == "subscribe")
            {
                return Results.Ok(hubChallenge);
            }

            return Results.Forbid();
        }

        private static async Task<IResult> NotifierHandler(IWebhookNotifierServices notifier, MessageReceived textMessage)
        {
            await notifier.NotifyEndpoints(textMessage);
            return Results.Ok();
        }
    }
}