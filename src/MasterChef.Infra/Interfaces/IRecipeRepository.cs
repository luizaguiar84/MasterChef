using MasterChef.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using MasterChef.Domain.Models;
using MasterChef.Dto;

namespace MasterChef.Infra.Interfaces
{
    public interface IRecipeRepository
    {
        Task<Recipe> AddAsync(RecipeDto recipe);
        Task<ResultDto<Recipe>> GetAll(RequestDto query);
        Task<Recipe> GetByIdAsync(int id);
        void Update(Recipe entity);
        Task<ResultDto<Recipe>> GetAllRecipesByUserId(RequestDto key, string id);
    }
}