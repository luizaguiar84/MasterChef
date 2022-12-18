using MasterChef.Infra.Enums;
using MasterChef.Infra.Interfaces;
using MasterChef.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using RestSharp;

namespace MasterChef.UI.Controllers
{
    [Authorize]
    public class RecipeController : Controller
    {
        private readonly IRestRequestClient _requestClient;

        private readonly string _pathImage;
        private readonly string _connection;

        public RecipeController(
            IWebHostEnvironment webHostEnvironment,
            IConfiguration configuration,
            IRestRequestClient requestClient)
        {
            _requestClient = requestClient;
            _pathImage = configuration["pathImagem"] ?? "";
            _connection = configuration["apiUrl"] ?? "";
        }

        [HttpGet]
        public async Task<IActionResult> Cadastro()
        {
            RecipeModel model = new RecipeModel();
            ViewBag.id = 0;

            var response = await _requestClient.GetJsonAsync<List<RecipeModel>>($"{_connection}/{Endpoints.Recipes}");

            model.Recipes = response;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastro(RecipeModel model)
        {
            if (!ModelState.IsValid)
                return View("Cadastro", model);

            RestResponse response = null;
            model.Image = await SaveImage(model);

            if (model.Id == 0)
                response = await _requestClient.PostAsync($"{_connection}/{Endpoints.Recipes}", model);
            else
                response = await _requestClient.PutAsync($"{_connection}/{Endpoints.Recipes}", model);

            if (!response.IsSuccessful)
                return View();

            var responseData = response.Content.FromJson<RecipeModel>();
            return RedirectToAction("Cadastro");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            ViewBag.id = id;

            var responseData = await _requestClient.GetJsonAsync<RecipeModel>($"{_connection}/{Endpoints.Recipes}/{id}");
            var responseDataList =
                await _requestClient.GetJsonAsync<List<RecipeModel>>($"{_connection}/{Endpoints.Recipes}");

            responseData.Recipes = responseDataList ?? new List<RecipeModel>();

            return View("Cadastro", responseData);
        }


        [HttpGet]
        public async Task<JsonResult> BuscarPorId(int id)
        {
            var response = await _requestClient.GetJsonAsync<RecipeModel>($"{_connection}/{Endpoints.Recipes}/{id}");

            if (response != null)
                return Json(response);

            return Json(null);
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(RecipeModel model)
        {
            var response = await _requestClient.DeleteAsync($"{_connection}/{Endpoints.Recipes}/{model.Id}");
            return RedirectToAction("Cadastro");
        }


        [NonAction]
        private async Task<string> SaveImage(RecipeModel model)
        {
            if (model.File == null)
            {
                var random = new Random();
                model.Image = $"imagem{random.Next(1, 10)}.jpg";
            }
            else
                model.Image = model.File.FileName;

            var path = Path.Combine(Directory.GetCurrentDirectory(), _pathImage);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var fileNameWithPath = Path.Combine(path, model.Image);

            if (model.File != null)
            {
                await using var stream = new FileStream(fileNameWithPath, FileMode.Create);
                await model.File.CopyToAsync(stream);
            }

            return model.Image ?? "";
        }
    }
}