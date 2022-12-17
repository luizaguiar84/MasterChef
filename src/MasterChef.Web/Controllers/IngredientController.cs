using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MasterChef.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace MasterChef.Web.Controllers
{
    public class IngredientController : Controller
    {
        private readonly string _connection;

        public IngredientController(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _connection = configuration["apiUrl"] ?? "";
        }

        [HttpGet]
        public async Task<IActionResult> BuscarPorReceitaId(int id)
        {
            IngredientModel model = new IngredientModel();
            ViewBag.ReceitaId = id;
            ViewBag.id = 0;
            using var client = new HttpClient();
            var response = await client.GetAsync($"{_connection}/Ingredient/{id}");
            var responseString = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseData = JsonConvert.DeserializeObject<List<IngredientModel>>(responseString);
                model.Ingredients = responseData ?? new List<IngredientModel>();
                return View("Cadastro", model);
            }
            return View("Cadastro", model);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastro(IngredientModel model)
        {

            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    if (model.Id == 0)
                    {
                        var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                        var response = await client.PostAsync($"{_connection}/Ingredient", content);
                        var responseString = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var responseData = JsonConvert.DeserializeObject<IngredientModel>(responseString);
                            return RedirectToAction($"BuscarPorReceitaId", new { id = responseData.RecipeId });
                        }
                    }
                    else
                    {
                        var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                        var response = await client.PutAsync($"{_connection}/Ingredient", content);
                        var responseString = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var responseData = JsonConvert.DeserializeObject<IngredientModel>(responseString);
                            return RedirectToAction($"BuscarPorReceitaId", new { id = responseData.RecipeId });
                        }
                    }

                    return View();
                }
            }
            return RedirectToAction($"BuscarPorReceitaId", new { id = ViewBag.ReceitaId });
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            ViewBag.id = id;

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_connection}/Ingredient");
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseData = JsonConvert.DeserializeObject<List<IngredientModel>>(responseString);
                    var dados = responseData.ToList().FirstOrDefault(x => x.Id == id) ?? new IngredientModel();

                    var responseList = await client.GetAsync($"{_connection}/Ingredient/{dados.RecipeId}");
                    var responseStringList = await responseList.Content.ReadAsStringAsync();
                    var responseDataList = JsonConvert.DeserializeObject<List<IngredientModel>>(responseStringList);
                    dados.Ingredients = responseDataList ?? new List<IngredientModel>();

                    ViewBag.ReceitaId = dados.RecipeId;
                    return View("Cadastro", dados);
                }
                return RedirectToAction($"BuscarPorReceitaId", new { id = ViewBag.ReceitaId });
            }
        }

        [HttpGet]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            ViewBag.id = id;

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_connection}/Ingredient");
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseData = JsonConvert.DeserializeObject<List<IngredientModel>>(responseString);
                    var dados = responseData.ToList().FirstOrDefault(x => x.Id == id) ?? new IngredientModel();

                    ViewBag.ReceitaId = dados.RecipeId;
                    return Json(dados);
                }
                return Json(null);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Excluir(IngredientModel model)
        {
            using (var client = new HttpClient())
            {
                var response = await client.DeleteAsync($"{_connection}/Ingredient/{model.Id}");
                var responseString = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseIngrediente = await client.GetAsync($"{_connection}/Ingredient");
                    var responseStringIngrediente = await responseIngrediente.Content.ReadAsStringAsync();
                    var responseDataIngrediente = JsonConvert.DeserializeObject<List<IngredientModel>>(responseStringIngrediente);
                    var dados = responseDataIngrediente.ToList().FirstOrDefault() ?? new IngredientModel();

                    return RedirectToAction($"BuscarPorReceitaId", new { id = dados.RecipeId });
                }
                return Json(null);
            }
        }
    }
}
