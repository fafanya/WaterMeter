using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WaterMeter.Common.Models;
using WaterMeter.Models;

namespace WaterMeter.Services
{
    public class BackendDataStore : IDataStore<TMeasurement>
    {
        HttpClient client;
        IEnumerable<TMeasurement> items;

        public BackendDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.BackendUrl}/");

            items = new List<TMeasurement>();
        }

        public async Task<IEnumerable<TMeasurement>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh)
            {
                var json = await client.GetStringAsync($"api/item");
                items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<TMeasurement>>(json));
            }

            return items;
        }

        public async Task<TMeasurement> GetItemAsync(int id)
        {
            if (id != 0)
            {
                var json = await client.GetStringAsync($"api/item/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<TMeasurement>(json));
            }

            return null;
        }

        public async Task<bool> AddItemAsync(TMeasurement item)
        {
            if (item == null)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);

            var response = await client.PostAsync($"api/item", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemAsync(TMeasurement item)
        {
            if (item == null || item.TMeasurementId == 0)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/item/{item.TMeasurementId}"), byteContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            if (id == 0)
                return false;

            var response = await client.DeleteAsync($"api/item/{id}");

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AddPhotoAsync(MeasurementLocal measurement)
        {
            MultipartFormDataContent content = new MultipartFormDataContent
            {
                { new StreamContent(new MemoryStream(measurement.Photo)), "\"file\"", $"\"{measurement.PhotoPath}\"" },
                { new StringContent(JsonConvert.SerializeObject(measurement.Details), Encoding.UTF8, "application/json"), "item" }
            };

            var response = await client.PostAsync($"api/item/CreateCounterMeasure/", content);
            return response.IsSuccessStatusCode;
        }
    }
}