using System.Collections.Generic;
using System.Threading.Tasks;
using MasterChef.Domain.Entities;
using MasterChef.Domain.Models;

namespace MasterChef.Application.Interfaces;

public interface IIngredientAppService
{
    Task<Ingredient> AddAsync(Ingredient Ingredient);
    Task UpdateAsync(Ingredient ingredient);
    Task<ResultDto<Ingredient>> GetByRecipeId(RequestDto query, int recipeId);
    Task Delete(int id);
    Task<ResultDto<Ingredient>> GetAll(RequestDto query);
}