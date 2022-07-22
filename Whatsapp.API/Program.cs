using Microsoft.AspNetCore.Mvc;
using Whatsapp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<HttpClient>();
builder.Services.AddSingleton<IMessageServices, MessageServices>();
builder.Services.AddSingleton<IWebhookNotifier, WebhookNotifier>();
builder.Services.AddSingleton<ITextMessageReceivedService, TextMessageReceivedServices>();
builder.Services.AddSingleton<IMediaHandler, MediaHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDirectoryBrowser();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/returnFile/{fileName}", ([FromRoute] string fileName) =>
{
    return Results.File(Path.Combine(builder.Environment.ContentRootPath, "StaticFiles", fileName), "image/jpg", $"{fileName}");
});

app.MapGet("/health", () => "estou vivo");

app.MapPost("/saveMedia", (IMessageServices handler, [FromBody] string mediaBase64) =>
{
    return handler.SaveMediaJpg(mediaBase64);
});

app.MapGet("/subscribe", (IWebhookNotifier notifier, string endpoint) =>
{
    notifier.Add(endpoint);
});

app.MapPost("/middlewareWebhook", async (IWebhookNotifier notifier, object textMessage) =>
{
    await notifier.NotifyEndpoints(textMessage);
});

app.MapGet("/middlewareWebhook",
(IConfiguration configuration,
[FromQuery(Name = "hub.mode")] string hubMode,
[FromQuery(Name = "hub.challenge")] int hubChallenge,
[FromQuery(Name = "hub.verify_token")] string hubVerifyToken) =>
{
    if (hubVerifyToken == configuration["VerifyToken"] && hubMode == "subscribe")
    {
        return Results.Ok(hubChallenge);
    }

    return Results.Forbid();
});

app.MapPost("/message", async (IMessageServices msgServices, TextMessageVM message) =>
{
    return await msgServices.SendMessage(message);
});

app.MapPost("/handleMessage", async (ITextMessageReceivedService textMessageServices, TextMessageReceived textMessage) =>
{
    await textMessageServices.HandleMessage(textMessage);
});

app.MapPost("/sendMediaByUrl", async (IMessageServices messageServices, MediaVM image) =>
{
    return await messageServices.SendMediaByUrl(image);
});

app.Run();