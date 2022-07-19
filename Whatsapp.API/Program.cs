using Whatsapp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IMessageServices, MessageServices>();
builder.Services.AddScoped<IWebhookNotifier, WebhookNotifier>();

builder.Services.AddScoped<ITextMessageReceivedServices, TextMessageReceivedServices>();
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

app.MapPost("/sendMessage", async (IMessageServices msgServices, TextMessageVM message) =>
{
    return await msgServices.SendMessage(message);
});

app.MapGet("/messagesWebhook", (string hub_mode, int hub_challenge, string hub_verify_token) =>
{
    return hub_challenge;
});

app.MapPost("/receiveMessage", async (ITextMessageReceivedServices textMessageServices, TextMessageReceived textMessage) =>
{
    await textMessageServices.GetMessage(textMessage);
});

app.Run();