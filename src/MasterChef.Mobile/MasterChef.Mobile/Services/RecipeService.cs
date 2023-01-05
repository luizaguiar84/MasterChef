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
        private IImagemService iimagemService;
        private IIngredientesService ingredientesService;
        public RecipeService(IConnectionService service, IImagemService iimagemService, IIngredientesService ingredientesService)
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
                var url = service.GetUrl("/api/recipes");
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
            var models = new List<RecipeModel>();

            var client = service.GetClient();
            var url = service.GetUrl("/api/recipes");
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
                        models = JsonConvert.DeserializeObject<IEnumerable<RecipeModel>>(responseString.Result).ToList();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                models = iimagemService.MontarImagem(models);
                models = ingredientesService.MontarIngredientes(models);
            }
            return models;
        }

        public bool Salvar(RecipeModel recipe)
        {
            try
            {
                var client = service.GetClient();
                var url = service.GetUrl("/api/recipes");
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
