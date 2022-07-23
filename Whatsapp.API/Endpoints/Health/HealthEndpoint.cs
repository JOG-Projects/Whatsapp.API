namespace Whatsapp.API.Endpoints.Health
{
    public class HealthEndpoint : IEndpointDefinition
    {
        public void DefineEndpoints(WebApplication app)
        {
            app.MapGet("/health", () => Results.Ok("estou vivo"));
        }
    }
}