using Newtonsoft.Json;

namespace Whatsapp.Services.Extensions
{
    internal static class JsonExtensions
    {
        public static T? TryJsonDeserialize<T>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}