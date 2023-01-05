using System.Collections.Generic;
using System.Threading.Tasks;
using MasterChef.Domain.Entities;

namespace MasterChef.Application.Interfaces;

public interface IIngredientAppService
{
    Task<Ingredient> Save(Ingredient Ingredient);
    Task<Ingredient> Update(Ingredient ingredient);
    Task<List<Ingredient>> GetByRecipeId(int recipeId);
    Task<bool> Delete(int id);
    Task<List<Ingredient>> GetAll();
}