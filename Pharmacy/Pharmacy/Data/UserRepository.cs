using Newtonsoft.Json;
using Pharmacy.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Data
{
    public class UserRepository
    {
        private readonly string UserUrl;
        private readonly HttpClient client;

        public UserRepository()
        {
            ResourceManager rm = new ResourceManager("Pharmacy.AppResources", typeof(UserRepository).Assembly);
            string url = rm.GetString("ApiConnection");
            string controller = rm.GetString("UserController");
            UserUrl = url != null && controller != null ? new StringBuilder(url).Append("/").Append(controller).ToString()
                                : throw new NullReferenceException($"Failed to create connection string: {url}");
            client = new HttpClient();
            App.Current.Properties.TryGetValue("token", out object jwt);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.ToString());
        }

        public async Task<string> Delete(UserDto user)
        {
            string json = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(new StringBuilder(UserUrl).Append("/").Append("Delete").ToString(), content);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> Login(UserDto user)
        {
            string json = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(new StringBuilder(UserUrl).Append("/").Append("Login").ToString(), content);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            return null;
        }

        public async Task<string> Register(UserDto user)
        {
            string json = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(new StringBuilder(UserUrl).Append("/").Append("Register").ToString(), content);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
