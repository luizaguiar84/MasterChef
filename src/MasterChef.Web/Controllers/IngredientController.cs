using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MasterChef.Infra.Interfaces;
using MasterChef.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using NuGet.Protocol;
using RestSharp;

namespace MasterChef.Web.Controllers
{
    public class IngredientController : Controller
    {
        private readonly IRestRequestClient _requestClient;
        private readonly string _connection;

        public IngredientController(
            IWebHostEnvironment webHostEnvironment,
            IConfiguration configuration,
            IRestRequestClient requestClient)
        {
            _requestClient = requestClient;
            _connection = configuration["apiUrl"] ?? "";
        }

        [HttpGet]
        public async Task<IActionResult> BuscarPorReceitaId(int id)
        {
            IngredientModel model = new IngredientModel();
            ViewBag.ReceitaId = id;
            ViewBag.id = 0;

            var response = await _requestClient.GetJsonAsync<List<IngredientModel>>($"{_connection}/Ingredient/{id}");
            model.Ingredients = response ?? new List<IngredientModel>();

            return View("Cadastro", model);

        }

        [HttpPost]
        public async Task<IActionResult> Cadastro(IngredientModel model)
        {
            if (ModelState.IsValid)
            {
                RestResponse response = null;
                if (model.Id == 0)
                    response = await _requestClient.PostAsync($"{_connection}/Ingredient", model);
                
                else
                    response = await _requestClient.PutAsync($"{_connection}/Ingredient", model);
                
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

            var response = await _requestClient.GetAsync($"{_connection}/Ingredient");

            if (response.IsSuccessful)
            {
                var responseData = response.Content.FromJson<List<IngredientModel>>();

                var dados = responseData.ToList().FirstOrDefault(x => x.Id == id) ?? new IngredientModel();

                var responseList = await _requestClient.GetJsonAsync<List<IngredientModel>>($"{_connection}/Ingredient/{dados.RecipeId}");

                dados.Ingredients = responseList ?? new List<IngredientModel>();

                ViewBag.ReceitaId = dados.RecipeId;
                return View("Cadastro", dados);
            }
            return RedirectToAction($"BuscarPorReceitaId", new { id = ViewBag.ReceitaId });

        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            ViewBag.id = id;
            
            var response = await _requestClient.GetAsync($"{_connection}/Ingredient");

            if (response.IsSuccessful)
            {
                var responseData = response.Content.FromJson<List<IngredientModel>>();
                var dados = responseData.ToList().FirstOrDefault(x => x.Id == id) ?? new IngredientModel();
                ViewBag.ReceitaId = dados.RecipeId;
                return Json(dados);
            }
            return Json(null);

        }


        [HttpPost]
        public async Task<IActionResult> Delete(IngredientModel model)
        {

            var response = await _requestClient.DeleteAsync($"{_connection}/Ingredient/{model.Id}");

            if (response.IsSuccessStatusCode)
            {
                var responseIngrediente = await _requestClient.GetJsonAsync<List<IngredientModel>>($"{_connection}/Ingredient");
                var dados = responseIngrediente.ToList().FirstOrDefault() ?? new IngredientModel();
                return RedirectToAction($"BuscarPorReceitaId", new { id = dados.RecipeId });
            }
            return Json(null);

        }
    }
}
