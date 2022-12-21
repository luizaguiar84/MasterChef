using MasterChef.Domain.Entities;
using MasterChef.Infra.Context;
using MasterChef.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MasterChef.Dto;

namespace MasterChef.Infra.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public RecipeRepository(
            DatabaseContext context,
            IMapper mapper
            )
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Recipe> AddAsync(RecipeDto recipe)
        {
            var entityDomain = _mapper.Map<Recipe>(recipe);

            entityDomain.CreateDate = DateTime.Now;
            entityDomain.LastChange = DateTime.Now;
            
            await _context.Recipes.AddAsync(entityDomain);

            return entityDomain;
        }

        public async Task<IList<Recipe>> GetAll()
        {
            var recipes = await _context.Recipes
                .AsNoTracking()
                .ToListAsync();
            
            return recipes;
        }
        
        public async Task<Recipe> GetByIdAsync(int id)
        {
            var recipe = await _context.Recipes
                .Include(r => r.Ingredients)
                .FirstOrDefaultAsync(r => r.Id == id && r.Active);

            return recipe;
        }

        public void Update(Recipe entity)
        {
            entity.LastChange = DateTime.Now;
            var response = _context.Recipes.Update(entity);
            _context.Entry(entity).Property(p => p.CreateDate).IsModified = false;
        }

        public async Task<List<Recipe>> GetAllRecipesByUserId(string id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.ExternalId == id);
            
            if (user == null)
                return null;

            var response = await _context.Recipes
                .AsNoTracking()
                .Where(r => r.UserId == user.Id && r.Active)
                .ToListAsync();
            
            return response;
        }
    }
}
