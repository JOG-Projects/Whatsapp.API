using Whatsapp.Domain;
using Whatsapp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IMessageServices, MessageServices>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/sendMessage", async (IMessageServices msgServices, TextMessageVM message) =>
{
    return await msgServices.SendMessage(message);
});

app.MapGet("/messagesWebhook", (string hub_mode, int hub_challenge, string hub_verify_token) => 
{
    return hub_challenge;
});

app.Run();