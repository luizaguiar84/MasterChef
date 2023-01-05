using System;
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

        public async Task<Ingredient> Add(Ingredient ingredient)
        {
            ingredient.CreateDate = DateTime.Now;
            ingredient.LastChange = DateTime.Now;

            await _context.Ingredients.AddAsync(ingredient);
            await _context.SaveChangesAsync();

            return ingredient;

        }

        public async Task<List<Ingredient>> GetAll()
        {
            var response = await _context.Ingredients.ToListAsync();
            return response;
        }

        public async Task<Ingredient> GetById(int id)
        {
            var response = await _context.Ingredients
                .FirstOrDefaultAsync(r => r.Id == id && r.Active);

            return response;
        }

        public async Task<Ingredient> Update(Ingredient entity)
        {
            entity.LastChange = DateTime.Now;
            _context.Ingredients.Update(entity);

            _context.Entry(entity).Property(p => p.CreateDate).IsModified = false;

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await GetById(id);

            if (entity == null)
                return false;

            _context.Ingredients.Remove(entity);
            var response = await _context.SaveChangesAsync();

            return response > 0;

        }
    }
}
