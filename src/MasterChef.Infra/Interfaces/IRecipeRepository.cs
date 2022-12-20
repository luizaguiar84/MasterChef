using MasterChef.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using MasterChef.Dto;

namespace MasterChef.Infra.Interfaces
{
    public interface IRecipeRepository
    {
        Task<Recipe> Add(RecipeDto recipe);
        Task<IList<Recipe>> GetAll();
        Task<Recipe> GetById(int id);
        Task<Recipe> Update(Recipe entity);
        Task<List<Recipe>> GetAllByUserId(string id);
    }
}