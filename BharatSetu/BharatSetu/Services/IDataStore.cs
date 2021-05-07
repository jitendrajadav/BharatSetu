using BharatSetu.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BharatSetu.Services
{
    public interface IDataStore<T>
    {
        Task<HttpResponseMessage> BeneficiaryAuthentication(Mobile mobile);
        Task<HttpResponseMessage> ConfirmAuthentication(ConfirmAuthentication confirm);

        Task<HttpResponseMessage> GetAllStates(string acceptLanguage);
        Task<HttpResponseMessage> GetDistrictsByStatesId(string acceptLanguage,string stateId);
        
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
