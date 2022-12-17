using System.Collections.Generic;
using System.Threading.Tasks;
using MasterChef.Domain.Entities;

namespace MasterChef.Infra.Interfaces;

public interface IIngredientRepository
{
    Task<List<Ingredient>> GetByRecipeId(int recipeId);
}