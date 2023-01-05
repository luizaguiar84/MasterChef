using MasterChef.Domain.Entities;
using MasterChef.Infra.Context;
using MasterChef.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MasterChef.Domain.Models;
using MasterChef.Infra.Helpers.ExtensionMethods;
using MasterChef.Dto.Dto;
using MasterChef.Dto.Resources;
using MasterChef.Dto.ResponseDto;

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

        public async Task<Recipe> AddAsync(Recipe entityDomain)
        {

            entityDomain.CreateDate = DateTime.Now;
            entityDomain.LastChange = DateTime.Now;

            await _context.Recipes.AddAsync(entityDomain);

            return entityDomain;
        }

        public async Task<ResultDto<Recipe>> GetAll(RecipeRequestDto query)
        {
            var queryable = _context.Recipes
                .Include(r => r.Ingredients)
                .AsNoTracking();

            if (!query.active.HasValue || query.active.HasValue && query.active.Value)
                queryable = queryable.Where(r => r.Active);

            var recipes = await queryable.ToListAsync(query);

            return recipes;
        }

        public async Task<Recipe> GetByIdAsync(int id)
        {
            var recipe = await _context.Recipes
                .Include(r => r.Ingredients)
                .FirstOrDefaultAsync(r => r.Id == id);

            return recipe;
        }

        public void Update(Recipe entity)
        {
            entity.LastChange = DateTime.Now;
            var response = _context.Recipes.Update(entity);
            _context.Entry(entity).Property(p => p.CreateDate).IsModified = false;
        }

        public async Task<ResultDto<Recipe>> GetAllRecipesByUserId(RecipeRequestDto key, string id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.ExternalId == id);

            if (user == null)
                return null;

            var query = _context.Recipes
                .AsNoTracking();

            if (!key.active.HasValue || key.active.HasValue && key.active.Value)
                query = query.Where(r => r.Active);
            
            var recipes = await query.ToListAsync(key);

            return recipes;
        }
    }
}