using RestSharp;

namespace MasterChef.Infra.Clients
{
    class RequestClientFactory
    {
        internal static RestClient GetClient()
        {
            var client = new RestClient();
            client.AddDefaultHeader("Accept", "Application/json");
            return client;
        }
    }
}
