using MasterChef.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MasterChef.Application.Interfaces;
using MasterChef.Domain.Interface;
using MasterChef.Domain.Models;
using MasterChef.Infra.Cache;
using MasterChef.Infra.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace MasterChef.Application.Services
{
    public class IngredientAppAppService : IIngredientAppService
    {
        private readonly IEventService _eventService;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IUnitOfWork _unitOfWork;

        public IngredientAppAppService(
            IEventService eventService,
            IIngredientRepository ingredientRepository,
            IUnitOfWork unitOfWork)
        {
            this._eventService = eventService;
            this._ingredientRepository = ingredientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Ingredient> AddAsync(Ingredient ingredient)
        {
            ingredient.CreateDate = DateTime.Now;
            ingredient.LastChange = DateTime.Now;
            await _ingredientRepository.AddAsync(ingredient);
            await _unitOfWork.CompleteAsync();

            return ingredient;
        }

        public async Task UpdateAsync(Ingredient ingredient)
        {
            _ingredientRepository.Update(ingredient);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<ResultDto<Ingredient>> GetByRecipeId(RequestDto query, int recipeId)
        {
            return await _ingredientRepository.GetByRecipeId(query, recipeId);
        }

        public async Task Delete(int id)
        {
            await _ingredientRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<ResultDto<Ingredient>> GetAll(RequestDto query)
        {
            return await _ingredientRepository.GetAll(query);
        }

        private string GetCacheKeyForIngredientQuery(RequestDto query, int id = 0)
        {
            var key = CacheKeys.IngredientsList.ToString();

            if (id > 0)
                key = string.Concat(key, "_", id);

            key = string.Concat(key, "_", query.Page, "_", query.PageSize);

            return key;
        }
    }
}