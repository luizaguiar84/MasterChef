using System.Collections.Generic;
using System.Threading.Tasks;
using MasterChef.Domain.Entities;

namespace MasterChef.Infra.Interfaces;

public interface IIngredientRepository
{
    Task<List<Ingredient>> GetByRecipeId(int recipeId);
    Task<Ingredient> Add(Ingredient ingredient);
    Task<List<Ingredient>> GetAll();
    Task<Ingredient> GetById(int id);
    Task<Ingredient> Update(Ingredient entity);
    Task<bool> Delete(int id);
}