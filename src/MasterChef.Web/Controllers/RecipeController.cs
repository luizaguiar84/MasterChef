using System.Collections.Generic;
using System.IO;
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
    public class RecipeController : Controller
    {
        private readonly IRestRequestClient _requestClient;

        private readonly string _pathImage;
        private readonly string _connection;
        private const string EndpointName = "Recipes";

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

            var response = await _requestClient.GetJsonAsync<List<RecipeModel>>($"{_connection}/{EndpointName}");

            model.Recipes = response;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastro(RecipeModel model)
        {
            if (!ModelState.IsValid)
                return View("Cadastro", model);

            RestResponse response = null;
            model.Picture = await SaveImage(model);

            if (model.Id == 0)
                response = await _requestClient.PostAsync($"{_connection}/{EndpointName}", model);
            else
                response = await _requestClient.PutAsync($"{_connection}/{EndpointName}", model);

            if (!response.IsSuccessful)
                return View();

            var responseData = response.Content.FromJson<RecipeModel>();
            return RedirectToAction("Cadastro");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            ViewBag.id = id;

            var responseData = await _requestClient.GetJsonAsync<RecipeModel>($"{_connection}/{EndpointName}/{id}");
            var responseDataList =
                await _requestClient.GetJsonAsync<List<RecipeModel>>($"{_connection}/{EndpointName}");

            responseData.Recipes = responseDataList ?? new List<RecipeModel>();

            return View("Cadastro", responseData);
        }


        [HttpGet]
        public async Task<JsonResult> BuscarPorId(int id)
        {
            var response = await _requestClient.GetJsonAsync<RecipeModel>($"{_connection}/{EndpointName}/{id}");

            if (response != null)
                return Json(response);

            return Json(null);
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(RecipeModel model)
        {
            var response = await _requestClient.DeleteAsync($"{_connection}/{EndpointName}/{model.Id}");
            return RedirectToAction("Cadastro");
        }


        [NonAction]
        private async Task<string> SaveImage(RecipeModel model)
        {
            if (model.File == null)
                return model.Picture ?? "";

            var path = Path.Combine(Directory.GetCurrentDirectory(), _pathImage);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var fileInfo = new FileInfo(model.File.FileName);
            model.Picture = model.File.FileName;

            var fileNameWithPath = Path.Combine(path, model.Picture);

            await using var stream = new FileStream(fileNameWithPath, FileMode.Create);
            await model.File.CopyToAsync(stream);

            return model.Picture ?? "";
        }
    }
}