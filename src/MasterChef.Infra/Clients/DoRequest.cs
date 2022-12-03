using RestSharp;

namespace MasterChef.Application.Clients
{
	public class DoRequest
	{
		public static IRestResponse Post(string caminhoRequisicao, object obj)
		{
			var client = RequestClientFactory.GetClient();
			var request = new RestRequest(caminhoRequisicao);
			request.AddJsonBody(obj);

			return client.Post(request);
		}
	}
}
