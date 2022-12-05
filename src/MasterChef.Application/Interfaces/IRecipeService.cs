using MasterChef.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasterChef.Application.Interfaces;

public interface IRecipeService
{
    void CreateNewRecipe(Recipe newRecipe);
    Task<IList<Recipe>> GetAll();
    Task<Recipe> GetById(int id);
    Task UpdateRecipe(Recipe recipe);
}