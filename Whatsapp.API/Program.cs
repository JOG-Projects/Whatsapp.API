using Whatsapp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<HttpClient>();
builder.Services.AddSingleton<IMessageServices, MessageServices>();
builder.Services.AddSingleton<IWebhookNotifier, WebhookNotifier>();
builder.Services.AddSingleton<ITextMessageReceivedService, TextMessageReceivedServices>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/health", () => "estou vivo");


app.MapGet("subscribe", (IWebhookNotifier notifier, string endpoint) =>
{
    notifier.Add(endpoint);
});

app.MapPost("/middlewareWebhook", async (IWebhookNotifier notifier, TextMessageReceived textMessage) =>
{
    await notifier.NotifyEndpoints(textMessage);
});

app.MapGet("/middlewareWebhook", (string hub_mode, int hub_challenge, string hub_verify_token) =>
{
    return hub_challenge;
});


app.MapPost("/message", async (IMessageServices msgServices, TextMessageVM message) =>
{
    return await msgServices.SendMessage(message);
});


app.MapPost("/handleMessage", async (ITextMessageReceivedService textMessageServices, TextMessageReceived textMessage) =>
{
    await textMessageServices.HandleMessage(textMessage);
});

app.Run();