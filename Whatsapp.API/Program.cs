using Whatsapp.Domain;
using Whatsapp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IMessageServices, MessageServices>();
builder.Services.AddScoped<ITextMessageReceivedServices, TextMessageReceivedServices>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/test", () => "testado");

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