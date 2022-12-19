using MasterChef.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasterChef.Infra.Interfaces
{
    public interface IRecipeRepository
    {
        Task<Recipe> Add(Recipe newRecipe);
        Task<IList<Recipe>> GetAll();
        Task<Recipe> GetById(int id);
        Task Update(Recipe entity);
        Task<List<Recipe>> GetAllByUserId(string id);
    }
}