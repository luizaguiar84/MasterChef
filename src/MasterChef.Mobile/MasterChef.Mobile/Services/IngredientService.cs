using MasterChef.Mobile.Interface;
using MasterChef.Mobile.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MasterChef.Mobile.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IConnectionService _service;

        public IngredientService(IConnectionService service)
        {
            this._service = service;
        }

        public bool Update(IngredientModel model)
        {
            try
            {
                using var client = _service.GetClient();
                var url = _service.GetUrl("/api/Ingredient");
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                client.Timeout = new TimeSpan(0, 0, 30);

                var response = client.PutAsync(url, content);

                return response.Result.IsSuccessStatusCode;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool Add(IngredientModel model)
        {
            try
            {
                using var client = _service.GetClient();
                var url = _service.GetUrl("/api/Ingredient");
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                client.Timeout = new TimeSpan(0, 0, 30);

                var response = client.PostAsync(url, content);

                return response.Result.IsSuccessStatusCode;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool Delete(int id)
        {
            using var client = _service.GetClient();
            var url = _service.GetUrl($"/api/Ingredient/{id}");
            var response = client.DeleteAsync(url);
            return response.Result.IsSuccessStatusCode;
            return false;
        }

        public List<IngredientModel> GetById(int id)
        {
            var models = new ResultDto<IngredientModel>();
            using var client = _service.GetClient();

            var url = _service.GetUrl($"/api/Ingredient/getbyrecipeId/{id}");
            client.Timeout = new TimeSpan(0, 0, 30);

            var response = client.GetAsync(url);
            if (!response.Result.IsSuccessStatusCode) 
                return models.Items;
            
            try
            {
                var responseString = response.Result.Content.ReadAsStringAsync();
                models = JsonConvert.DeserializeObject<ResultDto<IngredientModel>>(responseString.Result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return models.Items;
        }

        public List<RecipeModel> MountIngredients(List<RecipeModel> recipe)
        {
            foreach (var item in recipe) 
                item.Ingredients = GetById(item.Id);
            
            return recipe;
        }
    }
}