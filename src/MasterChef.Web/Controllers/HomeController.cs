using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MasterChef.Web.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace  MasterChef.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _connection;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _connection = configuration["apiUrl"] ?? "";
        }

        public async Task<IActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_connection}/Recipes");
                var responseString = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseData = JsonConvert.DeserializeObject<List<RecipeModel>>(responseString);
                    return View(responseData);
                }
                return View();
            }
        }

        [HttpGet]
        public async Task<JsonResult> BuscarPorId(int id)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_connection}/Recipes/{id}");
                var responseString = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseData = JsonConvert.DeserializeObject<RecipeModel>(responseString);
                    return Json(responseData);
                }
                return Json(null);
            }
        }
    }
}