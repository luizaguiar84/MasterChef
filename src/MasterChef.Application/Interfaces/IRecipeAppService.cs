using MasterChef.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using MasterChef.Domain.Models;
using MasterChef.Dto.Dto;
using MasterChef.Dto.Resources;
using MasterChef.Dto.ResponseDto;

namespace MasterChef.Application.Interfaces;

public interface IRecipeAppService
{
    Task<RecipeResponseDto> Save(RecipeDto recipe);
    Task<ResultDto<RecipeResponseDto>> GetAll(RecipeRequestDto query);
    Task<RecipeResponseDto> GetById(int id);
    Task<ResultDto<RecipeResponseDto>> GetAllByUserId(RecipeRequestDto key, string id);
    Task Update(RecipeDto recipe);
    Task<Recipe> Inactivate(int id);
}