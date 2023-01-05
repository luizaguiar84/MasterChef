using System;
using System.Net.Http;
using System.Text;
using MasterChef.Mobile.Interface;
using MasterChef.Mobile.Model;
using Newtonsoft.Json;

namespace MasterChef.Mobile.Services
{
    public class UserService : IUserService
    {
        private readonly IConnectionService _connection;

        public UserService(IConnectionService connection)
        {
            _connection = connection;
        }

        public bool VerifyLogin(string username, string password)
        {
            var client = _connection.GetClient();
            var url = _connection.GetUrl($"/api/user");

            using (client)
            {
                client.Timeout = new TimeSpan(0, 0, 30);
                client.DefaultRequestHeaders.Clear();

                var userContent = new UserModel()
                {
                    Password = password,
                    Username = username
                };

                var content = new StringContent(JsonConvert.SerializeObject(userContent), Encoding.UTF8, "application/json");

                var response = client.PostAsync(url, content);

                if (response.Result.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CreateNewUser(string email, string password)
        {
            var client = _connection.GetClient();
            var url = _connection.GetUrl($"/api/user/createUser");

            using (client)
            {
                client.Timeout = new TimeSpan(0, 0, 30);
                client.DefaultRequestHeaders.Clear();

                var userContent = new UserModel()
                {
                    Password = password,
                    Username = email
                };

                var content = new StringContent(JsonConvert.SerializeObject(userContent), Encoding.UTF8, "application/json");

                var response = client.PostAsync(url, content);

                return response.Result.IsSuccessStatusCode;
            }
        }
    }
}
