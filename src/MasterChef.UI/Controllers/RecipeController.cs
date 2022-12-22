using AutoMapper;
using MasterChef.Domain.Entities;
using MasterChef.Domain.Models;
using MasterChef.Infra.Enums;
using MasterChef.Infra.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using RestSharp;
using Microsoft.AspNetCore.Identity;
using MasterChef.Dto.Dto;

namespace MasterChef.UI.Controllers
{
    [Authorize]
    public class RecipeController : Controller
    {
        private readonly IRestRequestClient _requestClient;

        private readonly string _pathImage;
        private readonly string _connection;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public RecipeController(
            IConfiguration configuration,
            IRestRequestClient requestClient,
            UserManager<IdentityUser> userManager,
            IMapper mapper)
        {
            _requestClient = requestClient;
            _pathImage = configuration["pathImagem"] ?? "";
            _connection = configuration["apiUrl"] ?? "";
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Cadastro()
        {
            RecipeModel model = new RecipeModel();
            ViewBag.id = 0;

            var user = await GetUser();
            var response = await _requestClient.GetJsonAsync<ResultDto<RecipeModel>>($"{_connection}/{Endpoints.Recipes}/getRecipeByUser/{user.Id}");

            if (response != null)
                model.Recipes = response.Items;
               
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastro(RecipeModel model)
        {
            if (!ModelState.IsValid)
                return View("Cadastro", model);

            await UpdateUser(model);

            RestResponse? response = null;
            model.Image = await SaveImage(model);

            if (model.Id == 0)
                response = await _requestClient.PostAsync($"{_connection}/{Endpoints.Recipes}", _mapper.Map<RecipeDto>(model));
            else
                response = await _requestClient.PutAsync($"{_connection}/{Endpoints.Recipes}", _mapper.Map<RecipeDto>(model));

            if (!response.IsSuccessful)
                return View();

            var responseData = response.Content.FromJson<RecipeDto>();
            return RedirectToAction("Cadastro");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            ViewBag.id = id;

            var responseData = await _requestClient.GetJsonAsync<RecipeModel>($"{_connection}/{Endpoints.Recipes}/{id}");
            var responseDataList =
                await _requestClient.GetJsonAsync<ResultDto<RecipeModel>>($"{_connection}/{Endpoints.Recipes}");

            responseData.Recipes = responseDataList.Items ?? new List<RecipeModel>();

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
        
        private async Task UpdateUser(RecipeModel model)
        {
            if (model == null)
                return;
            
            var user = await GetUser();

            model.User = new User()
            {
                ExternalId = user.Id,
                Username = user.UserName,
                Password = ""
            };
        }

        private async Task<IdentityUser> GetUser()
        {
            var user = await _userManager.GetUserAsync(User);
            return user;
        }
    }
}