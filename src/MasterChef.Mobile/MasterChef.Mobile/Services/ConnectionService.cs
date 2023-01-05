using System;
using MasterChef.Mobile.Interface;
using System.Net.Http;
using MasterChef.Mobile.Model;
using Newtonsoft.Json;
using System.Data.Common;
using System.Net.Http.Headers;
using System.Text;

namespace MasterChef.Mobile.Services
{
    public class ConnectionService : IConnectionService
    {
        public HttpClient Client { get; set; }
        public string Url { get; set; }

        public ConnectionService()
        {
            Client = new HttpClient();
        }
        public HttpClient GetClient()
        {

#if DEBUG
            HttpClientHandler insecureHandler = GetInsecureHandler();
            HttpClient client = new HttpClient(insecureHandler);
#else
    HttpClient client = new HttpClient();
#endif
            try
            {
                var token = GetToken();

                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", $"{token}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return client;
        }

        private string GetToken()
        {

#if DEBUG
            HttpClientHandler insecureHandler = GetInsecureHandler();
            HttpClient client = new HttpClient(insecureHandler);
#else
    HttpClient client = new HttpClient();
#endif

            var token = string.Empty;
            using (client)
            {
                var url = GetUrl($"/api/token");

                client.Timeout = new TimeSpan(0, 0, 30);

                var userContent = new UserModel()
                {
                    Password = "senha",
                    Username = "api"
                };

                var content = new StringContent(JsonConvert.SerializeObject(userContent), Encoding.UTF8, "application/json");

                var response = client.PostAsync(url, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseString = response.Content.ReadAsStringAsync().Result;
                    token = (JsonConvert.DeserializeObject<TokenModel>(responseString)).Token;
                }
            }

            return token;
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
            var returnUrl = $"{url}{partialUrl}";
            return returnUrl;
        }
    }
}
