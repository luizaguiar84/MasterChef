using MasterChef.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using MasterChef.Dto;

namespace MasterChef.Application.Interfaces;

public interface IRecipeAppService
{
    Task<RecipeDto> Save(RecipeDto recipe);
    Task<List<Recipe>> GetAll();
    Task<Recipe> GetById(int id);
    Task<List<Recipe>> GetAllByUserId(string id);
    Task Update(RecipeDto recipe);
    Task<Recipe> Inactivate(int id);
}