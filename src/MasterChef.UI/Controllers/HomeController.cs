using AutoMapper;
using MasterChef.Domain.Models;
using MasterChef.Dto.Dto;
using MasterChef.Dto.ResponseDto;
using MasterChef.Infra.Enums;
using MasterChef.Infra.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace MasterChef.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRestRequestClient _requestClient;
        private readonly IMapper _mapper;
        private readonly string _connection;

        public HomeController(
            ILogger<HomeController> logger,
            IConfiguration configuration,
            IRestRequestClient requestClient,
            IMapper mapper)
        {
            _logger = logger;
            _requestClient = requestClient;
            _mapper = mapper;
            _connection = configuration["apiUrl"] ?? "";
        }

        public async Task<IActionResult> Index()
        {
            var response = await _requestClient.GetAsync($"{_connection}/{Endpoints.Recipes}");

            if (!response.IsSuccessful)
                return View(new List<RecipeModel>());
            
            var recipes = response.Content.FromJson<ResultDto<RecipeResponseDto>>();
            return View(_mapper.Map<List<RecipeModel>>(recipes.Items));
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