using System.Collections.Generic;
using System.IO;
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

    public class RecipeController : Controller
    {

        private readonly string _pathImage;
        private readonly string _connection;
        public RecipeController(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _pathImage = configuration["pathImagem"] ?? "";
            _connection = configuration["apiUrl"] ?? "";
        }

        [HttpGet]
        public async Task<IActionResult> Cadastro()
        {
            RecipeModel model = new RecipeModel();
            ViewBag.id = 0;

            using var client = new HttpClient();

            var response = await client.GetAsync($"{_connection}/Recipes");
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != HttpStatusCode.OK)
                return View();

            var responseData = JsonConvert.DeserializeObject<List<RecipeModel>>(responseString);
            model.Recipes = responseData;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastro(RecipeModel model)
        {

            if (ModelState.IsValid)
            {
                using var client = new HttpClient();

                if (model.Id == 0)
                {
                    model.Picture = await SaveImage(model);
                    var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync($"{_connection}/Recipes", content);
                    var responseString = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var responseData = JsonConvert.DeserializeObject<RecipeModel>(responseString);
                        return RedirectToAction("Cadastro");
                    }
                }
                else
                {
                    model.Picture = await SaveImage(model);
                    var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    var response = await client.PutAsync($"{_connection}/Recipes", content);
                    var responseString = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var responseData = JsonConvert.DeserializeObject<RecipeModel>(responseString);
                        return RedirectToAction("Cadastro");
                    }
                }

                return View();
            }
            return View("Cadastro", model);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            ViewBag.id = id;
            using var client = new HttpClient();

            var response = await client.GetAsync($"{_connection}/Receita/{id}");
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseList = await client.GetAsync($"{_connection}/Receita");
                var responseStringList = await responseList.Content.ReadAsStringAsync();
                var responseDataList = JsonConvert.DeserializeObject<List<RecipeModel>>(responseStringList);

                var responseData = JsonConvert.DeserializeObject<RecipeModel>(responseString);
                responseData.Recipes = responseDataList ?? new List<RecipeModel>();
                return View("Cadastro", responseData);
            }
            return View();
        }


        [HttpGet]
        public async Task<JsonResult> BuscarPorId(int id)
        {
            using var client = new HttpClient();

            var response = await client.GetAsync($"{_connection}/Recipes/{id}");
            var responseString = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseData = JsonConvert.DeserializeObject<RecipeModel>(responseString);
                return Json(responseData);
            }
            return Json(null);
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(RecipeModel model)
        {
            using var client = new HttpClient();

            var response = await client.DeleteAsync($"{_connection}/Recipes/{model.Id}");
            var responseString = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction("Cadastro");
            }
            return Json(null);
        }



        [NonAction]
        public async Task<string> SaveImage(RecipeModel model)
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
