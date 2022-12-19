using MasterChef.Domain.Entities;
using MasterChef.Infra.Context;
using MasterChef.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Recipe> Add(Recipe recipe)
        {
            recipe.CreateDate = DateTime.Now;
            recipe.LastChange = DateTime.Now;

            await _context.Recipes.AddAsync(recipe);
            await _context.SaveChangesAsync();

            return recipe;

        }

        public async Task<IList<Recipe>> GetAll()
        {
            var recipes = await _context.Recipes.ToListAsync();
            return recipes;
        }
        
        public async Task<Recipe> GetById(int id)
        {
            var recipe = await _context.Recipes
                .Include(r => r.Ingredients)
                .FirstOrDefaultAsync(r => r.Id == id && r.Active);

            return recipe;
        }

        public async Task Update(Recipe entity)
        {
            entity.LastChange = DateTime.Now;
            _context.Recipes.Update(entity);
            
            _context.Entry(entity).Property(p => p.CreateDate).IsModified = false;

            await _context.SaveChangesAsync();
        }

        public async Task<List<Recipe>> GetAllByUserId(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.ExternalId == id);
            if (user == null)
                return null;

            var response = await _context.Recipes.Where(r => r.UserId == user.Id && r.Active).ToListAsync();
            return response;
        }
    }
}
