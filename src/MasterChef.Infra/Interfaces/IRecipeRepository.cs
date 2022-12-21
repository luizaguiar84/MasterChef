using MasterChef.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using MasterChef.Dto;

namespace MasterChef.Infra.Interfaces
{
    public interface IRecipeRepository
    {
        Task<Recipe> AddAsync(RecipeDto recipe);
        Task<IList<Recipe>> GetAll();
        Task<Recipe> GetByIdAsync(int id);
        void Update(Recipe entity);
        Task<List<Recipe>> GetAllRecipesByUserId(string id);
    }
}