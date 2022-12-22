using System.Threading.Tasks;
using MasterChef.Dto.Dto;
using MasterChef.Dto.Resources;
using MasterChef.Dto.ResponseDto;

namespace MasterChef.Application.Interfaces;

public interface IIngredientAppService
{
    Task<IngredientResponseDto> AddAsync(IngredientDto ingredient);
    Task UpdateAsync(IngredientDto ingredient);
    Task<ResultDto<IngredientResponseDto>> GetByRecipeId(RequestDto query, int recipeId);
    Task Delete(int id);
    Task<ResultDto<IngredientResponseDto>> GetAll(RequestDto query);
}