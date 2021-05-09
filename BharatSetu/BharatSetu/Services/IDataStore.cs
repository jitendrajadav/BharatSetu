using BharatSetu.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BharatSetu.Services
{
    public interface IDataStore<T>
    {
        Task<HttpResponseMessage> GenerateOTP(Mobile mobile);
        Task<HttpResponseMessage> ConfirmOTP(ConfirmAuthentication confirm);

        Task<HttpResponseMessage> States(string acceptLanguage);
        Task<HttpResponseMessage> Districts(string acceptLanguage, string stateId);

        Task<HttpResponseMessage> FindByPin(string acceptLanguage, string pincode, string date);
        Task<HttpResponseMessage> FindByDistrict(string acceptLanguage, string distId, string date);
        Task<HttpResponseMessage> CalanderByPin(string acceptLanguage, string pincode, string date);
        Task<HttpResponseMessage> CalanderByDistrict(string acceptLanguage, string distId, string date);

        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
