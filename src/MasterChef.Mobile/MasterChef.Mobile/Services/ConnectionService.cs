using MasterChef.Mobile.Interface;
using System.Net.Http;

namespace MasterChef.Mobile.Services
{
    public class ConnectionService : IConnectionService
    {
        public HttpClient client { get; set; }
        public string url { get; set; }

        public ConnectionService()
        {
            client = new HttpClient();
        }
        public HttpClient GetClient()
        {

#if DEBUG
            HttpClientHandler insecureHandler = GetInsecureHandler();
            HttpClient client = new HttpClient(insecureHandler);
#else
    HttpClient client = new HttpClient();
#endif
            return client;
        }
        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }

        public string GetUrl(string partialUrl)
        {
             string url = "https://10.0.2.2:7043";
            var urlretorno = $"{url}{partialUrl}";
            return urlretorno;
        }
    }
}
