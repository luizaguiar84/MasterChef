using MasterChef.Mobile.Interface;
using MasterChef.Mobile.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MasterChef.Mobile.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IConnectionService _service;
        private readonly IImageService _imageService;
        private readonly IIngredientService _ingredientService;
        public RecipeService(
            IConnectionService service,
            IImageService imageService,
            IIngredientService ingredientService)
        {
            this._imageService = imageService;
            this._service = service;
            this._ingredientService = ingredientService;
        }

        public bool Update(RecipeModel recipe)
        {
            try
            {
                using var client = _service.GetClient();
                var url = _service.GetUrl("/api/recipe");
                var content = new StringContent(JsonConvert.SerializeObject(recipe), Encoding.UTF8, "application/json");
                client.Timeout = new TimeSpan(0, 0, 30);

                var response = client.PutAsync(url, content);

                return response.Result.IsSuccessStatusCode;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool Delete(RecipeModel recipe)
        {
            using var client = _service.GetClient();
            var url = _service.GetUrl($"/api/recipe/{recipe.Id}");
            var response = client.DeleteAsync(url);

            return response.Result.IsSuccessStatusCode;


            return false;
        }

        public List<RecipeModel> GetAll()
        {
            var models = new ResultDto<RecipeModel>();

            using var client = _service.GetClient();
            var url = _service.GetUrl("/api/recipe");
            client.Timeout = new TimeSpan(0, 0, 30);

            var response = client.GetAsync(url);
            if (response.Result.IsSuccessStatusCode)
            {
                try
                {
                    var responseString = response.Result.Content.ReadAsStringAsync();
                    models = JsonConvert.DeserializeObject<ResultDto<RecipeModel>>(responseString.Result);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            models.Items = _imageService.MountImage(models.Items);
            models.Items = _ingredientService.MountIngredients(models.Items);
            return models.Items;
        }

        public bool Save(RecipeModel recipe)
        {
            try
            {
                using var client = _service.GetClient();
                var url = _service.GetUrl("/api/recipe");
                var content = new StringContent(JsonConvert.SerializeObject(recipe), Encoding.UTF8, "application/json");
                client.Timeout = new TimeSpan(0, 0, 30);

                var response = client.PostAsync(url, content);

                return response.Result.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
