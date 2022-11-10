using Newtonsoft.Json;
using Pharmacy.Data.Abstract;
using Pharmacy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Pharmacy.Data
{
    public class MedicineRepository : IRepository<Medicine>
    {
        private readonly string MedicineUrl;
        private readonly HttpClient client;
        private object token;

        public MedicineRepository()
        {                     
            ResourceManager rm = new ResourceManager("Pharmacy.AppResources", typeof(MedicineRepository).Assembly);
            string url = rm.GetString("ApiConnection");
            string controller = rm.GetString("MedicineController");
            MedicineUrl = url != null && controller != null ? new StringBuilder(url).Append("/").Append(controller).ToString()
                                : throw new NullReferenceException($"Failed to create connection string: {url}");
            client = new HttpClient();
            Application.Current.Properties.TryGetValue("token", out token);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());
        }

        public async Task<string> Delete(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync(new StringBuilder(MedicineUrl).Append("/").Append(id).ToString());
            return await response.Content.ReadAsStringAsync();
        }
            public async Task<IEnumerable<Medicine>> GetAll()
        {
            HttpResponseMessage response = await client.GetAsync(MedicineUrl);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Medicine>>(content);
            }
            return new List<Medicine>();
        }

        public async Task<Medicine> GetById(int id)
        {
            HttpResponseMessage response = await client.GetAsync(new StringBuilder(MedicineUrl).Append("/").Append(id).ToString());
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Medicine>(content);
            }
            return null;
        }

        public async Task<string> Insert(Medicine item)
        {
            string json = JsonConvert.SerializeObject(item);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(MedicineUrl, content);
            return await response.Content.ReadAsStringAsync();
        }

        public IEnumerable<Medicine> Search(string query)
        {
            var manufacturers = Task.Run(async () => await UnitOfWork.GetUnitOfWork().ManufacturerRepository.GetAll()).Result;
            return Task.Run(async () => await GetAll()).Result
                .Join(manufacturers, med => med.ManufacturerId, manuf => manuf.ManufacturerId, (med, manuf) => new { med, manuf.Title })
                .Where(x =>
                x.med.Title.ToLower().Contains(query.ToLower())
            || x.med.Barcode.ToLower().Contains(query.ToLower())
            || x.Title.ToLower().Contains(query.ToLower()
            )).Select(x => x.med).OrderByDescending(x => x.Title).ThenByDescending(x => x.Barcode);
        }

        public async Task<string> Update(Medicine item)
        {
            string json = JsonConvert.SerializeObject(item);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync(new StringBuilder(MedicineUrl).Append("/").Append(item.MedicineId).ToString(), content);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
