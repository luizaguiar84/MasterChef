using RestSharp;

namespace MasterChef.Application.Clients
{
    class RequestClientFactory
    {
        private const string PathApi = "https://api.paghiper.com/";

        public static RestClient GetClient()
        {
            var client = new RestClient(PathApi);
            client.AddDefaultHeader("Accept", "Application/json");
            return client;
        }
    }
}
