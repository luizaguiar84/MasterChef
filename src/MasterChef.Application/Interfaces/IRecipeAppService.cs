using MasterChef.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using MasterChef.Domain.Models;
using MasterChef.Dto;

namespace MasterChef.Application.Interfaces;

public interface IRecipeAppService
{
    Task<RecipeDto> Save(RecipeDto recipe);
    Task<ResultDto<Recipe>> GetAll(RequestDto query);
    Task<Recipe> GetById(int id);
    Task<ResultDto<Recipe>> GetAllByUserId(RequestDto key, string id);
    Task Update(RecipeDto recipe);
    Task<Recipe> Inactivate(int id);
}