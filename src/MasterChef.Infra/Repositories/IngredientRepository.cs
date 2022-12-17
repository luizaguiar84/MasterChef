using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterChef.Domain.Entities;
using MasterChef.Infra.Context;
using MasterChef.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MasterChef.Infra.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly DatabaseContext _context;

        public IngredientRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Ingredient>> GetByRecipeId(int recipeId)
        {
            var response = await _context.Ingredients.Where(
                    i => i.RecipeId == recipeId)
                .ToListAsync();

            return response;
        }
    }
}
