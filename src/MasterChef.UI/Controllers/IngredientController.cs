using AutoMapper;
using MasterChef.Domain.Models;
using MasterChef.Dto.Dto;
using MasterChef.Dto.ResponseDto;
using MasterChef.Infra.Enums;
using MasterChef.Infra.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using RestSharp;

namespace MasterChef.UI.Controllers
{
    public class IngredientController : Controller
    {
        private readonly IRestRequestClient _requestClient;
        private readonly IMapper _mapper;
        private readonly string _connection;

        public IngredientController(
            IWebHostEnvironment webHostEnvironment,
            IConfiguration configuration,
            IRestRequestClient requestClient,
            IMapper mapper)
        {
            _requestClient = requestClient;
            _mapper = mapper;
            _connection = configuration["apiUrl"] ?? "";
        }

        [HttpGet]
        public async Task<IActionResult> BuscarPorReceitaId(int id)
        {
            IngredientModel model = new IngredientModel();
            ViewBag.ReceitaId = id;
            ViewBag.id = 0;

            var response = await _requestClient.GetJsonAsync<ResultDto<IngredientResponseDto>>($"{_connection}/{Endpoints.Ingredient}/getbyrecipeid/{id}");

            var ingredients = _mapper.Map<List<IngredientModel>>(response.Items);
            
            model.Ingredients = ingredients;

            return View("Cadastro", model);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastro(IngredientModel model)
        {
            if (ModelState.IsValid)
            {
                RestResponse? response = null;
                if (model.Id == 0)
                    response = await _requestClient.PostAsync($"{_connection}/{Endpoints.Ingredient}", model);
                
                else
                    response = await _requestClient.PutAsync($"{_connection}/{Endpoints.Ingredient}", model);
                
                if (response != null && response.IsSuccessful)
                {
                    var responseData = response.Content.FromJson<IngredientModel>();
                    return RedirectToAction($"BuscarPorReceitaId", new { id = responseData.RecipeId });
                }

                return View();
            }
            return RedirectToAction($"BuscarPorReceitaId", new { id = ViewBag.ReceitaId });
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            ViewBag.id = id;

            var response = await _requestClient.GetAsync($"{_connection}/{Endpoints.Ingredient}/{id}");

            if (response.IsSuccessful)
            {
                var responseData = response.Content.FromJson<IngredientModel>();

                var responseList = await _requestClient.GetJsonAsync<ResultDto<IngredientModel>>($"{_connection}/{Endpoints.Ingredient}/getbyrecipeid/{responseData.RecipeId}");

                responseData.Ingredients = responseList.Items ?? new List<IngredientModel>();

                ViewBag.ReceitaId = responseData.RecipeId;
                return View("Cadastro", responseData);
            }
            return RedirectToAction($"BuscarPorReceitaId", new { id = ViewBag.ReceitaId });
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            ViewBag.id = id;
            
            var response = await _requestClient.GetAsync($"{_connection}/{Endpoints.Ingredient}/{id}");

            if (!response.IsSuccessful) 
                return Json(null);

            var responseData = response.Content.FromJson<IngredientModel>();
            ViewBag.ReceitaId = responseData.RecipeId;
            return Json(responseData);

        }


        [HttpPost]
        public async Task<IActionResult> Delete(IngredientModel model)
        {

            var path = $"{_connection}/{Endpoints.Ingredient}/{model.Id}";
            
            var response = await _requestClient.DeleteAsync(path);

            if (response.IsSuccessStatusCode)
            {
                var responseIngrediente = await _requestClient.GetJsonAsync<ResultDto<IngredientModel>>($"{_connection}/{Endpoints.Ingredient}");
                var dados = responseIngrediente.Items.FirstOrDefault() ?? new IngredientModel();
                return RedirectToAction($"BuscarPorReceitaId", new { id = dados.RecipeId });
            }
            return Json(null);

        }
    }
}
