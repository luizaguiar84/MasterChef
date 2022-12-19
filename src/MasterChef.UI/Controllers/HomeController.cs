using MasterChef.Infra.Enums;
using MasterChef.Infra.Interfaces;
using MasterChef.UI.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace MasterChef.UI.Controllers
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
            var response = await _requestClient.GetAsync($"{_connection}/{Endpoints.Recipes}");

            if (!response.IsSuccessful)
                return View(new List<RecipeModel>());

            var recipes = response.Content.FromJson<List<RecipeModel>>();
            return View(recipes);
        }

        [HttpGet]
        public async Task<JsonResult> GetById(int id)
        {
            var response = await _requestClient.GetAsync($"{_connection}/{Endpoints.Recipes}/{id}");

            if (!response.IsSuccessStatusCode)
                return Json(null);

            var recipe = response.Content.FromJson<RecipeModel>();
            return Json(recipe);
        }
    }
}