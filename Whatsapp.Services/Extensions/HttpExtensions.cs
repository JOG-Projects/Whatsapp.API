using Newtonsoft.Json;
using System.Text;

namespace Whatsapp.Services.Extensions
{
    internal static class HttpExtensions
    {
        public static async Task<string> PostJsonAsync(this HttpClient httpClient, string endpoint, object body) 
        {
            var response = await httpClient.PostAsync(endpoint, new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"));

            return await response.Content.ReadAsStringAsync();
        }
    }
}