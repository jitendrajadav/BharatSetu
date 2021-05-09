using BharatSetu.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BharatSetu.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items;
        readonly HttpClient client;

        public MockDataStore()
        {
            client = new HttpClient();
            items = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." }
            };
        }

        public async Task<HttpResponseMessage> GenerateOTP(Mobile mobile)
        {
            Uri uri = new Uri(string.Format(Constants.BaseUrl + Constants.GenerateOTP));
            string json = JsonConvert.SerializeObject(mobile);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            return await client.PostAsync(uri, content);
        }

        public async Task<HttpResponseMessage> ConfirmOTP(ConfirmAuthentication confirm)
        {
            Uri uri = new Uri(string.Format(Constants.BaseUrl + Constants.ConfirmOTP));
            string json = JsonConvert.SerializeObject(confirm);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            return await client.PostAsync(uri, content);
        }

        public async Task<HttpResponseMessage> States(string acceptLanguage)
        {
            Uri uri = new Uri(string.Format(Constants.BaseUrl + Constants.States));
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("Accept-Language", acceptLanguage);
            return await client.GetAsync(uri);
        }

        public async Task<HttpResponseMessage> Districts(string acceptLanguage, string stateId)
        {
            Uri uri = new Uri(string.Format(Constants.BaseUrl + Constants.Districts, stateId));
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("Accept-Language", acceptLanguage);
            return await client.GetAsync(uri);
        }

        public async Task<HttpResponseMessage> FindByPin(string acceptLanguage, string pincode, string date)
        {
            Uri uri = new Uri(string.Format(Constants.BaseUrl + Constants.FindByPin, pincode,date));
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("Accept-Language", acceptLanguage);
            return await client.GetAsync(uri);
        }

        public async Task<HttpResponseMessage> FindByDistrict(string acceptLanguage, string distId, string date)
        {
            Uri uri = new Uri(string.Format(Constants.BaseUrl + Constants.FindByDistrict, distId, date));
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("Accept-Language", acceptLanguage);
            return await client.GetAsync(uri);
        }

        public async Task<HttpResponseMessage> CalanderByPin(string acceptLanguage, string pincode, string date)
        {
            Uri uri = new Uri(string.Format(Constants.BaseUrl + Constants.CalanderByPin, pincode, date));
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("Accept-Language", acceptLanguage);
            return await client.GetAsync(uri);
        }

        public async Task<HttpResponseMessage> CalanderByDistrict(string acceptLanguage, string distId, string date)
        {
            Uri uri = new Uri(string.Format(Constants.BaseUrl + Constants.CalendarByDistrict, distId, date));
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("Accept-Language", acceptLanguage);
            return await client.GetAsync(uri);
        }

        public async Task<HttpResponseMessage> Download(string beneficiary_reference_id)
        {
            Uri uri = new Uri(string.Format(Constants.BaseUrl + Constants.Download, beneficiary_reference_id));
            return await client.GetAsync(uri);
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

    }
}