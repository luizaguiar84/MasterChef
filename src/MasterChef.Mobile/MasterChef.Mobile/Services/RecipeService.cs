using MasterChef.Mobile.Interface;
using MasterChef.Mobile.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace MasterChef.Mobile.Services
{
    public class RecipeService : IRecipeService
    {
        private IConnectionService service;
        private IImageService iimagemService;
        private IIngredientesService ingredientesService;
        public RecipeService(IConnectionService service, IImageService iimagemService, IIngredientesService ingredientesService)
        {
            this.iimagemService = iimagemService;
            this.service = service;
            this.ingredientesService = ingredientesService;
        }

        public bool Atualiza(RecipeModel recipe)
        {
            try
            {
                var client = service.GetClient();
                var url = service.GetUrl("/api/recipe");
                var content = new StringContent(JsonConvert.SerializeObject(recipe), Encoding.UTF8, "application/json");
                using (var cliente = client)
                {
                    cliente.Timeout = new TimeSpan(0, 0, 30);
                    cliente.DefaultRequestHeaders.Clear();

                    var response = cliente.PutAsync(url, content);
                    if (response.Result.IsSuccessStatusCode)
                    {
                        if (response.Result.IsSuccessStatusCode)
                            return true;
                        else
                            return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool Delete(RecipeModel recipe)
        {
            var client = service.GetClient();
            var url = service.GetUrl($"/api/recipes/{recipe.Id}");
            using (var cliente = client)
            {
                var response = cliente.DeleteAsync(url);
                if (response.Result.IsSuccessStatusCode)
                {
                    if (response.Result.IsSuccessStatusCode)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }

        public List<RecipeModel> GetAll()
        {
            var models = new ResultDto<RecipeModel>();

            var client = service.GetClient();
            var url = service.GetUrl("/api/recipe");
            using (var cliente = client)
            {
                cliente.Timeout = new TimeSpan(0, 0, 30);
                cliente.DefaultRequestHeaders.Clear();

                var response = cliente.GetAsync(url);
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
                models.Items = iimagemService.MountImage(models.Items);
                models.Items = ingredientesService.MontarIngredientes(models.Items);
            }
            return models.Items;
        }

        public bool Salvar(RecipeModel recipe)
        {
            try
            {
                var client = service.GetClient();
                var url = service.GetUrl("/api/recipe");
                var content = new StringContent(JsonConvert.SerializeObject(recipe), Encoding.UTF8, "application/json");
                using (var cliente = client)
                {
                    cliente.Timeout = new TimeSpan(0, 0, 30);
                    cliente.DefaultRequestHeaders.Clear();

                    var response = cliente.PostAsync(url, content);
                    if (response.Result.IsSuccessStatusCode)
                    {
                        if (response.Result.IsSuccessStatusCode)
                            return true;
                        else
                            return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
