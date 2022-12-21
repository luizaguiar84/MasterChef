using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterChef.Domain.Entities;
using MasterChef.Domain.Models;
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

        public async Task<ResultDto<Ingredient>> GetByRecipeId(RequestDto key, int recipeId)
        {
            var query = 
                _context.Ingredients
                    .AsNoTracking()
                    .Where(i => i.RecipeId == recipeId);
            
            var totalItems = await query.CountAsync();

            var ingredients =  await query
                .Skip((key.Page - 1) * key.PageSize)
                .Take(key.PageSize)
                .ToListAsync();

            return new ResultDto<Ingredient>()
            {
                TotalItems = totalItems,
                Items = ingredients
            };
        }

        public async Task<Ingredient> AddAsync(Ingredient ingredient)
        {
            ingredient.CreateDate = DateTime.Now;
            ingredient.LastChange = DateTime.Now;

            await _context.Ingredients.AddAsync(ingredient);

            return ingredient;
        }

        public async Task<ResultDto<Ingredient>> GetAll(RequestDto key)
        {
            var query = _context.Ingredients
                .AsNoTracking();
            
            var totalItems = await query.CountAsync();

            var ingredients =  await query
                .Skip((key.Page - 1) * key.PageSize)
                .Take(key.PageSize)
                .ToListAsync();

            return new ResultDto<Ingredient>()
            {
                TotalItems = totalItems,
                Items = ingredients
            };
        }

        public async Task<Ingredient> GetByIdAsync(int id)
        {
            var response = await _context.Ingredients
                .FirstOrDefaultAsync(r => r.Id == id && r.Active);

            return response;
        }

        public void Update(Ingredient entity)
        {
            entity.LastChange = DateTime.Now;
            _context.Ingredients.Update(entity);
            _context.Entry(entity).Property(p => p.CreateDate).IsModified = false;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            _context.Ingredients.Remove(entity);
        }
    }
}