using System.Threading.Tasks;
using RestSharp;

namespace MasterChef.Application.Clients
{
	public class DoRequest
	{
		public static async Task<RestResponse> Post(string path, object obj = null)
		{
			var client = RequestClientFactory.GetClient();
			var request = new RestRequest(path);
			
			if (obj != null)
				request.AddJsonBody(obj);

			return await client.PostAsync(request);
		}
		
		public static async Task<RestResponse> Get(string path)
		{
			var client = RequestClientFactory.GetClient();
			var request = new RestRequest(path);

			return await client.GetAsync(request);
		}
	}
}
