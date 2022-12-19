using System.Threading.Tasks;
using MasterChef.Infra.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using ILogger = Serilog.ILogger;

namespace MasterChef.Infra.Clients
{
    public class RestRequestClient : IRestRequestClient
    {
        private readonly ILogger _logger;
        public RequestClientFactory RequestFactory { get; set; }

        public RestRequestClient(ILogger logger)
        {
            _logger = logger;
            RequestFactory = new RequestClientFactory();
        }


        public async Task<RestResponse> PostAsync(string path, object obj = null)
		{
			var client = await RequestFactory.GetClient();
			var request = new RestRequest(path);
			
			if (obj != null)
				request.AddJsonBody(obj);

            var x = JsonConvert.SerializeObject(obj);
            _logger.Information("Post: {@x}", x);
			return await client.PostAsync(request);
		}

        public async Task<T> GetJsonAsync<T>(string path)
        {
            var client = await RequestFactory.GetClient();
            var request = new RestRequest(path);
            
            return await client.GetJsonAsync<T>(path);
        }

        public async Task<RestResponse> GetAsync(string path)
        {
			var client = await RequestFactory.GetClient();
			var request = new RestRequest(path);

			return await client.GetAsync(request);
		}

        public async Task<RestResponse> PutAsync(string path, object obj = null)
        {
            var client = await RequestFactory.GetClient();
            var request = new RestRequest(path);

            if (obj != null)
                request.AddJsonBody(obj);

            return await client.PutAsync(request);
        }

        public async Task<RestResponse> DeleteAsync(string path, object obj = null)
        {
            var client = await RequestFactory.GetClient();
            var request = new RestRequest(path);

            if (obj != null)
                request.AddJsonBody(obj);

            return await client.DeleteAsync(request);
        }

    }
}
