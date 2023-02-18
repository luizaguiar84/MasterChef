using MasterChef.Domain.Entities;
using System.Threading.Tasks;
using MasterChef.Dto.Dto;
using MasterChef.Dto.Resources;

namespace MasterChef.Infra.Interfaces
{
    public interface IRecipeRepository
    {
        Task<Recipe> AddAsync(Recipe recipe);
        Task<ResultDto<Recipe>> GetAll(RecipeRequestDto query);
        Task<Recipe> GetByIdAsync(int id);
        void Update(Recipe entity);
        Task<ResultDto<Recipe>> GetAllRecipesByUserId(RecipeRequestDto key, string id);
    }
}