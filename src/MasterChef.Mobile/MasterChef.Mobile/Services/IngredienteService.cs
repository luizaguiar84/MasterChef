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
    public class IngredienteService : IIngredientesService
    {
        private readonly IConnectionService service;

        public IngredienteService(IConnectionService service)
        {
            this.service = service;
        }

        public bool AtualizarIngrediente(IngredienteModel model)
        {
            try
            {
                var client = service.GetClient();
                var url = service.GetUrl("/api/Ingredient");
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
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

        public bool CadastrerIngrediente(IngredienteModel model)
        {
            try
            {
                var client = service.GetClient();
                var url = service.GetUrl("/api/Ingredient");
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
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

        public bool Deletar(int id)
        {
            var client = service.GetClient();
            var url = service.GetUrl($"/api/Ingredient/{id}");
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

        public List<IngredienteModel> GetById(int id)
        {
            var models = new List<IngredienteModel>();
            var client = service.GetClient();
            var url = service.GetUrl($"/api/Ingredient/{id}");
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
                        models = JsonConvert.DeserializeObject<IEnumerable<IngredienteModel>>(responseString.Result).ToList();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return models;
        }

        public List<RecipeModel> MontarIngredientes(List<RecipeModel> recipe)
        {
            foreach (var item in recipe)
            {
                item.Ingredients = GetById(item.Id);
            }
            return recipe;
        }
    }
}