using Newtonsoft.Json;
using Pharmacy.Data.Abstract;
using Pharmacy.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Data
{
    public class ManufacturerRepository : IRepository<Manufacturer>
    {
        private readonly string manufacturerUrl;
        private readonly HttpClient client;

        public ManufacturerRepository()
        {
            ResourceManager rm = new ResourceManager("Pharmacy.AppResources", typeof(ManufacturerRepository).Assembly);
            string url = rm.GetString("ApiConnection");
            string controller = rm.GetString("ManufacturerController");
            manufacturerUrl = url != null && controller != null ? new StringBuilder(url).Append('/').Append(controller).ToString()
                                : throw new NullReferenceException("Failed to create connection string");
            client = new HttpClient();
        }

        public async Task<bool> Delete(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync(new StringBuilder(manufacturerUrl).Append("/").Append(id).ToString());
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<IEnumerable<Manufacturer>> GetAll()
        {
            Uri uri = new Uri(manufacturerUrl);
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Manufacturer>>(content);
            }
            return new List<Manufacturer>();
        }

        public async Task<Manufacturer> GetById(int id)
        {
            HttpResponseMessage response = await client.GetAsync(new StringBuilder(manufacturerUrl).Append("/").Append(id).ToString());
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Manufacturer>(content);
            }
            return null;
        }

        public async Task<bool> Insert(Manufacturer obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(new UriBuilder(manufacturerUrl).Uri, content);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public IEnumerable<Manufacturer> Search(string query)
        {
            return Task.Run(async () => await GetAll()).Result.Where(i => 
            i.Title.ToLower().Contains(query.ToLower()) 
            || i.Phone.ToLower().Contains(query.ToLower()) 
            || i.Address.ToLower().Contains(query.ToLower()) 
            || i.Email.ToLower().Contains(query.ToLower()));
        }

        public async Task<bool> Update(Manufacturer obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync(new StringBuilder(manufacturerUrl).Append("/").Append(obj.ManufacturerId).ToString(), content);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }
    }
}
