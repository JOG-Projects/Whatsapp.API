using System.Reflection;

namespace Whatsapp.API.Endpoints
{
    public static class EndpointExtensions
    {
        public static void UseEndpointsDefinition(this WebApplication app)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var endpoints = GetEndpoints(assembly).Select(Activator.CreateInstance).Cast<IEndpointDefinition>();

            foreach (var endpoint in endpoints)
            {
                endpoint.DefineEndpoints(app);
            }
        }

        private static IEnumerable<Type> GetEndpoints(Assembly assembly)
        {
            return assembly.ExportedTypes.Where(t => typeof(IEndpointDefinition).IsAssignableFrom(t) && !t.IsInterface);
        }
    }

    public interface IEndpointDefinition
    {
        void DefineEndpoints(WebApplication app);
    }
}