using System.Collections.Generic;
using System.Threading.Tasks;
using MasterChef.Domain.Entities;

namespace MasterChef.Infra.Interfaces;

public interface IIngredientRepository
{
    Task<List<Ingredient>> GetByRecipeId(int recipeId);
    Task<Ingredient> AddAsync(Ingredient ingredient);
    Task<List<Ingredient>> GetAll();
    Task<Ingredient> GetByIdAsync(int id);
    void Update(Ingredient entity);
    Task DeleteAsync(int id);
}