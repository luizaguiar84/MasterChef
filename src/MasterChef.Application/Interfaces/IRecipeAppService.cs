using MasterChef.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasterChef.Application.Interfaces;

public interface IRecipeAppService
{
    Task<Recipe> Save(Recipe recipe);
    Task<List<Recipe>> GetAll();
    Task<Recipe> GetById(int id);
    Task<Recipe> Update(Recipe recipe);
    Task<Recipe> Inactivate(int id);
}