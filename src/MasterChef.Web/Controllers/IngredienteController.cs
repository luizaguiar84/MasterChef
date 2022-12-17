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

namespace  MasterChef.Web.Controllers
{
    public class IngredienteController : Controller
    {
        private IWebHostEnvironment webHostEnvironment;
        private readonly IConfiguration configuration;
        private string conexao;

        public IngredienteController(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.configuration = configuration;
            conexao = configuration["apiUrl"] ?? "";
        }

        [HttpGet]
        public async Task<IActionResult> BuscarPorReceitaId(int id)
        {
            IngredienteModel model = new IngredienteModel();
            ViewBag.ReceitaId = id;
            ViewBag.id = 0;
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{conexao}/Ingrediente/{id}");
                var responseString = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseData = JsonConvert.DeserializeObject<List<IngredienteModel>>(responseString);
                    model.ingredientes = responseData?? new List<IngredienteModel>();
                    return View("Cadastro", model);
                }
                return View("Cadastro", model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Cadastro(IngredienteModel model)
        {

            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    if (model.id == 0)
                    {
                        var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                        var response = await client.PostAsync($"{conexao}/Ingrediente", content);
                        var responseString = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var responseData = JsonConvert.DeserializeObject<IngredienteModel>(responseString);
                            return RedirectToAction($"BuscarPorReceitaId", new  {id = responseData.receitaId} );
                        }
                    }
                    else
                    {
                        var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                        var response = await client.PutAsync($"{conexao}/Ingrediente", content);
                        var responseString = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var responseData = JsonConvert.DeserializeObject<IngredienteModel>(responseString);
                            return RedirectToAction($"BuscarPorReceitaId", new {id = responseData.receitaId});
                        }
                    }

                    return View();
                }
            }
            return RedirectToAction($"BuscarPorReceitaId", new {id = ViewBag.ReceitaId});
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            ViewBag.id = id;
 
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{conexao}/Ingrediente");
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseData = JsonConvert.DeserializeObject<List<IngredienteModel>>(responseString);
                    var dados = responseData.ToList().FirstOrDefault(x => x.id == id) ?? new IngredienteModel();

                    var responseList = await client.GetAsync($"{conexao}/Ingrediente/{dados.receitaId}");
                    var responseStringList = await responseList.Content.ReadAsStringAsync();
                    var responseDataList = JsonConvert.DeserializeObject<List<IngredienteModel>>(responseStringList);
                    dados.ingredientes = responseDataList ?? new List<IngredienteModel>();

                    ViewBag.ReceitaId = dados.receitaId;
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
                var response = await client.GetAsync($"{conexao}/Ingrediente");
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseData = JsonConvert.DeserializeObject<List<IngredienteModel>>(responseString);
                    var dados = responseData.ToList().FirstOrDefault(x => x.id == id) ?? new IngredienteModel();

                    ViewBag.ReceitaId = dados.receitaId;
                    return Json(dados);
                }
                return Json(null);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Excluir(IngredienteModel model)
        {
            using (var client = new HttpClient())
            {
                var response = await client.DeleteAsync($"{conexao}/Ingrediente/{model.id}");
                var responseString = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseIngrediente = await client.GetAsync($"{conexao}/Ingrediente");
                    var responseStringIngrediente = await responseIngrediente.Content.ReadAsStringAsync();
                    var responseDataIngrediente = JsonConvert.DeserializeObject<List<IngredienteModel>>(responseStringIngrediente);
                    var dados = responseDataIngrediente.ToList().FirstOrDefault() ?? new IngredienteModel();

                    return RedirectToAction($"BuscarPorReceitaId", new { id = dados.receitaId });
                }
                return Json(null);
            }
        }
    }
}
