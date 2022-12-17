using System.Threading.Tasks;
using MasterChef.Infra.Interfaces;
using RestSharp;

namespace MasterChef.Infra.Clients
{
    public class RestRequestClient : IRestRequestClient
    {
		public async Task<RestResponse> PostAsync(string path, object obj = null)
		{
			var client = RequestClientFactory.GetClient();
			var request = new RestRequest(path);
			
			if (obj != null)
				request.AddJsonBody(obj);

			return await client.PostAsync(request);
		}

        public async Task<T> GetJsonAsync<T>(string path)
        {
            var client = RequestClientFactory.GetClient();
            var request = new RestRequest(path);
            
            return await client.GetJsonAsync<T>(path);
        }

        public async Task<RestResponse> GetAsync(string path)
		{
			var client = RequestClientFactory.GetClient();
			var request = new RestRequest(path);

			return await client.GetAsync(request);
		}

        public async Task<RestResponse> PutAsync(string path, object obj = null)
        {
            var client = RequestClientFactory.GetClient();
            var request = new RestRequest(path);

            if (obj != null)
                request.AddJsonBody(obj);

            return await client.PutAsync(request);
        }

        public async Task<RestResponse> DeleteAsync(string path, object obj = null)
        {
            var client = RequestClientFactory.GetClient();
            var request = new RestRequest(path);

            if (obj != null)
                request.AddJsonBody(obj);

            return await client.DeleteAsync(request);
        }
    }
}
