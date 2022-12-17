using MasterChef.Application.Interfaces;
using MasterChef.Domain.Entities;
using MasterChef.Infra.Interfaces;
using MasterChef.Infra.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MasterChef.Domain.Interface;

namespace MasterChef.Application.Services;

public class RecipeAppAppService : IRecipeAppService
{
    private readonly IRepository<Recipe> _repository;
    private readonly IIngredientAppService _ingredientAppService;
    private readonly IValidator<Recipe> _validation;
    private readonly IEventService _eventService;
    private readonly IRecipeRepository _recipeRepository;
    public RecipeAppAppService(
        IRepository<Recipe> repository,
        IValidator<Recipe> validation,
        IEventService eventService,
        IRecipeRepository recipeRepository,
        IIngredientAppService ingredientAppService)
    {
        this._repository = repository;
        this._validation = validation;
        this._eventService = eventService;
        this._recipeRepository = recipeRepository;
        this._ingredientAppService = ingredientAppService;
    }

    public async Task<Recipe> Save(Recipe recipe)
    {
        var validator = await _validation.ValidateAsync(recipe);

        if (!validator.IsValid)
        {
            var events = await _eventService.Add("Save Recipe", validator.Errors);
        }
        else
        {
            recipe.CreateDate = DateTime.Now;
            var response = await _repository.Save(recipe);
        }

        return recipe;
    }
    public async Task<Recipe> Update(Recipe recipe)
    {
        await _repository.Update(recipe);
        return recipe;
    }

    public async Task<Recipe> GetById(int id)
    {
        return await _recipeRepository.GetById(id);
    }

    public async Task<List<Recipe>> GetAll()
    {
        var response =  await _repository.GetAll();
        return response.Where(r => r.Active).ToList();
    }

    public async Task<Recipe> Inactivate(int id)
    {
        var recipe = await _recipeRepository.GetById(id);
        recipe.Active = false;
        await _repository.Update(recipe);

        return recipe;
    }
}
