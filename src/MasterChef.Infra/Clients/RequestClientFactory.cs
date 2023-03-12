using System;
using System.IO;
using MasterChef.Domain.Entities;
using RestSharp;
using System.Threading.Tasks;
using MasterChef.Infra.Helpers;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp.Authenticators;

namespace MasterChef.Infra.Clients
{
   
    public class RequestClientFactory
    {
        private readonly IConfiguration _config;

        public RequestClientFactory() { }

        public async Task<RestClient> GetClient()
        {
            var token = await GetTokenAsync();

            var client = new RestClient();
            client.AddDefaultHeader("Accept", "Application/json");
            client.Authenticator = new JwtAuthenticator(token);
            return client;
        }

        public async Task<string> GetTokenAsync()
        {
            var client = new RestClient();
            client.AddDefaultHeader("Accept", "Application/json");

            var request = new RestRequest(GetTokenPath());

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

        private static string GetTokenPath()
        {
            var config = ConfigurationHelpers.GetConfiguration();

            return config["pathToken"] ?? string.Empty;
        }
    }
}
