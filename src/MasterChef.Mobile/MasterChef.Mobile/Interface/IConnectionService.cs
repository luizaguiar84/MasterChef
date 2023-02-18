using System.Net.Http;
    
namespace MasterChef.Mobile.Interface
{
    public interface IConnectionService
    {
        HttpClient Client { get; set; }
        string Url { get; set; }
        HttpClient GetClient();
        string GetUrl(string partialUrl);
        HttpClientHandler GetInsecureHandler();
    }
}
