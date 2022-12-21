using System.Collections.Generic;
using System.Threading.Tasks;
using MasterChef.Domain.Entities;

namespace MasterChef.Application.Interfaces;

public interface IIngredientAppService
{
    Task<Ingredient> AddAsync(Ingredient Ingredient);
    Task UpdateAsync(Ingredient ingredient);
    Task<List<Ingredient>> GetByRecipeId(int recipeId);
    Task Delete(int id);
    Task<List<Ingredient>> GetAll();
}