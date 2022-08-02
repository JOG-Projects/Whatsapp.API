using Whatsapp.API;
using Whatsapp.API.Endpoints;
using Whatsapp.Services.AutoMapperServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationDependencies();
builder.Services.AddAutoMapper(typeof(AutoMapperServicesConfiguration));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseEndpointsDefinition();

app.Run();