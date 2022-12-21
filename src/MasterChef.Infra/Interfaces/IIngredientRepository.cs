using System.Collections.Generic;
using System.Threading.Tasks;
using MasterChef.Domain.Entities;
using MasterChef.Domain.Models;

namespace MasterChef.Infra.Interfaces;

public interface IIngredientRepository
{
    Task<ResultDto<Ingredient>> GetByRecipeId(RequestDto key, int recipeId);
    Task<Ingredient> AddAsync(Ingredient ingredient);
    Task<ResultDto<Ingredient>> GetAll(RequestDto key);
    Task<Ingredient> GetByIdAsync(int id);
    void Update(Ingredient entity);
    Task DeleteAsync(int id);
}