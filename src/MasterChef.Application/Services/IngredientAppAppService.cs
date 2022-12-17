using MasterChef.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterChef.Application.Interfaces;
using MasterChef.Domain.Interface;
using MasterChef.Infra.Interfaces;
using MasterChef.Infra.Repositories;

namespace MasterChef.Application.Services
{
    public class IngredientAppAppService : IIngredientAppService
    {
        private readonly IRepository<Ingredient> _repository;
        private readonly IEventService _eventService;
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientAppAppService(IRepository<Ingredient> repository, IEventService eventService, IIngredientRepository IngredientRepository)
        {
            this._repository = repository;
            this._eventService = eventService;
            this._ingredientRepository = IngredientRepository;
        }
        public async Task<Ingredient> Save(Ingredient ingredient)
        {
            ingredient.CreateDate = DateTime.Now;
            ingredient.LastChange = DateTime.Now;
            await _repository.Save(ingredient);
            return ingredient;
        }
        public async Task<Ingredient> Update(Ingredient ingredient)
        {
            var response = GetAll().Result.FirstOrDefault(x => x.Id == ingredient.Id);
            
            if (response == null) 
                return response;

            response.Description = ingredient.Description;
            response.Name = ingredient.Name;
            response.Weight = ingredient.Weight;
            response.Quantity = ingredient.Quantity;
            response.LastChange = ingredient.LastChange;

            await _repository.Update(response);

            return response;
        }
        public async Task<List<Ingredient>> GetByRecipeId(int recipeId)
        {
            return await _ingredientRepository.GetByRecipeId(recipeId);
        }

        public async Task<bool> Delete(int id)
        {
            var response = await _repository.Delete(id);
            return response;
        }

        public async Task<List<Ingredient>> GetAll()
        {
            var response = await _repository.GetAll();
            return response;
        }
    }
}
