using System.Threading.Tasks;
using RestSharp;

namespace MasterChef.Infra.Interfaces;

public interface IRestRequestClient
{
    Task<RestResponse> DeleteAsync(string path, object obj = null);
    Task<RestResponse> PostAsync(string path, object obj = null);
    Task<RestResponse> PutAsync(string path, object obj = null);
    Task<RestResponse> GetAsync(string path);
    Task<T> GetJsonAsync<T>(string path);
}