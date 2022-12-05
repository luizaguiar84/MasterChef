using MasterChef.Application.Interfaces;
using MasterChef.Domain.Entities;
using MasterChef.Infra.Interfaces;
using MasterChef.Infra.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasterChef.Application.Services;

public class RecipeService : IRecipeService
{
    private readonly IRecipeRepository _repository;

    public RecipeService(IRecipeRepository repository)
    {
        this._repository = repository;

    }

    public void CreateNewRecipe(Recipe newRecipe)
    {
        _repository.Add(newRecipe);

    }

    public async Task<IList<Recipe>> GetAll()
    {
        var response = await _repository.GetAll();
        return response;
    }

    public async Task<Recipe> GetById(int id)
    {
        return await _repository.GetById(id);
    }

    public async Task UpdateRecipe(Recipe entity)
    {
        await _repository.Update(entity);

    }
}