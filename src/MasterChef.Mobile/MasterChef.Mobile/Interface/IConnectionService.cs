using System.Net.Http;
    
namespace MasterChef.Mobile.Interface
{
    public interface IConnectionService
    {
        HttpClient client { get; set; }
        string url { get; set; }
        HttpClient GetClient();
        string GetUrl(string partialUrl);
        HttpClientHandler GetInsecureHandler();
    }
}
