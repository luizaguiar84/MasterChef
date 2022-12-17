using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MasterChef.Infra.Interfaces;
using MasterChef.Web.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MasterChef.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRestRequestClient _requestClient;
        private readonly string _connection;
        public HomeController(
            ILogger<HomeController> logger,
            IConfiguration configuration,
            IRestRequestClient requestClient)
        {
            _logger = logger;
            _requestClient = requestClient;
            _connection = configuration["apiUrl"] ?? "";
        }

        public async Task<IActionResult> Index()
        {
            var response = await _requestClient.GetJsonAsync<List<RecipeModel>>($"{_connection}/Recipes");
            return View(response);
        }

        [HttpGet]
        public async Task<JsonResult> GetById(int id)
        {
            var response = await _requestClient.GetJsonAsync<RecipeModel>($"{_connection}/Recipes/{id}");
            return Json(response);
        }
    }
}