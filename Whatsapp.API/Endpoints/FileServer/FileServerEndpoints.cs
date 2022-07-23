using Microsoft.AspNetCore.Mvc;
using Whatsapp.Services.FileServer;
using Whatsapp.Services.MediaUpload;

namespace Whatsapp.API.Endpoints.FileServer
{
    public class FileServerEndpoints : IEndpointDefinition
    {
        public void DefineEndpoints(WebApplication app)
        {
            app.MapGet("/files/{fileName}", GetFile);

            app.MapPost("/saveJpg", SaveJpg);
        }

        private static IResult GetFile(IHostEnvironment env, [FromRoute] string fileName)
        {
            var path = Path.Combine(env.ContentRootPath, "images", fileName);
            return Results.File(path, "image/jpg");
        }

        private static IResult SaveJpg(IMediaServices mediaServices, ImageUploadRequestVM imageUpload)
        {
            return Results.Ok(mediaServices.SaveImage(imageUpload));
        }
    }
}