using MasterChef.Domain.Entities;
using MasterChef.Infra.Context;
using MasterChef.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasterChef.Infra.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly DatabaseContext _context;

        public RecipeRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task Add(Recipe recipe)
        {
            recipe.CreateDate = DateTime.Now;
            recipe.LastChange = DateTime.Now;

            await _context.Recipes.AddAsync(recipe);
            await _context.SaveChangesAsync();

        }

        public async Task<IList<Recipe>> GetAll()
        {
            var recipes = await _context.Recipes.ToListAsync();
            return recipes;
        }
        
        public async Task<Recipe> GetById(int id)
        {
            var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == id);
            return recipe;
        }

        public async Task Update(Recipe entity)
        {
            entity.LastChange = DateTime.Now;
            _context.Recipes.Update(entity);
            
            _context.Entry(entity).Property(p => p.CreateDate).IsModified = false;

            await _context.SaveChangesAsync();
        }
    }
}
