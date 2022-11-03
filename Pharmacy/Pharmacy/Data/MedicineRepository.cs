﻿using Newtonsoft.Json;
using Pharmacy.Data.Abstract;
using Pharmacy.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Data
{
    public class MedicineRepository : IRepository<Medicine>
    {
        private readonly string MedicineUrl;
        private readonly HttpClient client;

        public MedicineRepository()
        {
            ResourceManager rm = new ResourceManager("Pharmacy.AppResources", typeof(MedicineRepository).Assembly);
            string url = rm.GetString("ApiConnection");
            string controller = rm.GetString("MedicineController");
            MedicineUrl = url != null && controller != null ? new StringBuilder(url).Append("/").Append(controller).ToString()
                                : throw new NullReferenceException($"Failed to create connection string: {url}");
            client = new HttpClient();
        }

        public async Task<bool> Delete(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync(new StringBuilder(MedicineUrl).Append("/").Append(id).ToString());
            if (response.IsSuccessStatusCode)
                return true;
            return false;
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

        public async Task<bool> Insert(Medicine obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(MedicineUrl, content);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
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

        public async Task<bool> Update(Medicine obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync(new StringBuilder(MedicineUrl).Append("/").Append(obj.MedicineId).ToString(), content);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }
    }
}
