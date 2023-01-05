using MasterChef.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MasterChef.Application.Interfaces;
using MasterChef.Domain.Interface;
using MasterChef.Infra.Interfaces;

namespace MasterChef.Application.Services
{
    public class IngredientAppAppService : IIngredientAppService
    {
        private readonly IEventService _eventService;
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientAppAppService(IEventService eventService, IIngredientRepository ingredientRepository)
        {
            this._eventService = eventService;
            this._ingredientRepository = ingredientRepository;
        }
        public async Task<Ingredient> Save(Ingredient ingredient)
        {
            ingredient.CreateDate = DateTime.Now;
            ingredient.LastChange = DateTime.Now;
            await _ingredientRepository.Add(ingredient);
            return ingredient;
        }
        public async Task<Ingredient> Update(Ingredient ingredient)
        {
            var response = await _ingredientRepository.Update(ingredient);
            return response;
        }
        public async Task<List<Ingredient>> GetByRecipeId(int recipeId)
        {
            return await _ingredientRepository.GetByRecipeId(recipeId);
        }

        public async Task<bool> Delete(int id)
        {
            var response = await _ingredientRepository.Delete(id);
            return response;
        }

        public async Task<List<Ingredient>> GetAll()
        {
            var response = await _ingredientRepository.GetAll();
            return response;
        }
    }
}
