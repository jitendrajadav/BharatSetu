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
        private readonly List<Item> items;
        private readonly HttpClient client;

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

        public async Task<HttpResponseMessage> GenerateOTP(Mobile model)
        {
            Uri uri = new Uri(string.Format(Constants.BaseUrl + Constants.GenerateOTP));
            return await MakePostCall(model, uri);
        }

        public async Task<HttpResponseMessage> ConfirmOTP(ConfirmAuthentication model)
        {
            Uri uri = new Uri(string.Format(Constants.BaseUrl + Constants.ConfirmOTP));
            return await MakePostCall(model, uri);
        }

        public async Task<HttpResponseMessage> States(string acceptLanguage)
        {
            Uri uri = new Uri(string.Format(Constants.BaseUrl + Constants.States));
            return await MakeGetCall(acceptLanguage, uri);
        }

        public async Task<HttpResponseMessage> Districts(string acceptLanguage, string stateId)
        {
            Uri uri = new Uri(string.Format(Constants.BaseUrl + Constants.Districts, stateId));
            return await MakeGetCall(acceptLanguage, uri);
        }

        public async Task<HttpResponseMessage> FindByPin(string acceptLanguage, string pincode, string date)
        {
            Uri uri = new Uri(string.Format(Constants.BaseUrl + Constants.FindByPin, pincode,date));
            return await MakeGetCall(acceptLanguage, uri);
        }

        public async Task<HttpResponseMessage> FindByDistrict(string acceptLanguage, string distId, string date)
        {
            Uri uri = new Uri(string.Format(Constants.BaseUrl + Constants.FindByDistrict, distId, date));
            return await MakeGetCall(acceptLanguage, uri);
        }

        public async Task<HttpResponseMessage> CalanderByPin(string acceptLanguage, string pincode, string date)
        {
            Uri uri = new Uri(string.Format(Constants.BaseUrl + Constants.CalanderByPin, pincode, date));
            return await MakeGetCall(acceptLanguage, uri);
        }

        public async Task<HttpResponseMessage> CalanderByDistrict(string acceptLanguage, string distId, string date)
        {
            Uri uri = new Uri(string.Format(Constants.BaseUrl + Constants.CalendarByDistrict, distId, date));
            return await MakeGetCall(acceptLanguage, uri);
        }

        private async Task<HttpResponseMessage> MakeGetCall(string acceptLanguage, Uri uri)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("Accept-Language", acceptLanguage);
            return await client.GetAsync(uri);
        }

        private async Task<HttpResponseMessage> MakePostCall<T>(T model, Uri uri) where T : class
        {
            string json = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            return await client.PostAsync(uri, content);
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
            Item oldItem = items.FirstOrDefault((Item arg) => arg.Id == item.Id);
            _ = items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            Item oldItem = items.FirstOrDefault((Item arg) => arg.Id == id);
            _ = items.Remove(oldItem);

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