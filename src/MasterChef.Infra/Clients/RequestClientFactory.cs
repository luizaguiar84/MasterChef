using MasterChef.Domain.Entities;
using RestSharp;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp.Authenticators;


namespace MasterChef.Infra.Clients
{
    public class RequestClientFactory
    {
        internal async Task<RestClient> GetClient()
        {
            var token = await GetTokenAsync();

            var client = new RestClient();
            client.AddDefaultHeader("Accept", "Application/json");
            client.Authenticator = new JwtAuthenticator(token);
            return client;
        }

        private async Task<string> GetTokenAsync()
        {
            var client = new RestClient();
            client.AddDefaultHeader("Accept", "Application/json");

            var request = new RestRequest("https://localhost:7043/api/Token");

            request.AddJsonBody(new TokenInfo()
            {
                Username = "api",
                Password = "senha"
            });

            var response = await client.PostAsync(request);

            dynamic tokenResponse = null;

            if (response.IsSuccessStatusCode) 
                tokenResponse = JsonConvert.DeserializeObject<dynamic>(response.Content);

            return response.IsSuccessStatusCode ? tokenResponse.token : string.Empty;
        }
    }
}
