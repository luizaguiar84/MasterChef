using System;
using MasterChef.Application.Interfaces;
using MasterChef.Domain.Entities;
using MasterChef.Infra.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MasterChef.Domain.Interface;
using MasterChef.Domain.Models;
using MasterChef.Dto;
using MasterChef.Infra.Cache;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;

namespace MasterChef.Application.Services;

public class RecipeAppService : IRecipeAppService
{
    private readonly IIngredientAppService _ingredientAppService;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMemoryCache _cache;
    private readonly IValidator<RecipeDto> _validation;
    private readonly IEventService _eventService;
    private readonly IRecipeRepository _recipeRepository;

    public RecipeAppService(
        IValidator<RecipeDto> validation,
        IEventService eventService,
        IRecipeRepository recipeRepository,
        IIngredientAppService ingredientAppService,
        IUserRepository userRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IMemoryCache cache)
    {
        this._validation = validation;
        this._eventService = eventService;
        this._recipeRepository = recipeRepository;
        this._ingredientAppService = ingredientAppService;
        _userRepository = userRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _cache = cache;
    }

    public async Task<RecipeDto> Save(RecipeDto recipe)
    {
        var validator = await _validation.ValidateAsync(recipe);

        if (!validator.IsValid)
        {
            var events = await _eventService.Add("Save Recipe", validator.Errors);
            return null;
        }

        var user = await _userRepository.GetByExternalId(recipe.User?.ExternalId);

        if (user == null)
            user = await _userRepository.Add(_mapper.Map<User>(recipe.User));

        recipe.UserId = user.Id;

        var response = await _recipeRepository.AddAsync(recipe);
        await _unitOfWork.CompleteAsync();

        return _mapper.Map<RecipeDto>(response);
    }

    public async Task<ResultDto<Recipe>> GetAllByUserId(RequestDto key, string id)
    {
        var cacheKey = GetCacheKeyForRecipeQuery(key, id);

        var response = await _cache.GetOrCreateAsync(cacheKey, (entry) =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
            return _recipeRepository.GetAllRecipesByUserId(key, id);
        });

        return response;
    }

    public async Task Update(RecipeDto recipe)
    {
        var user = await _userRepository.GetByExternalId(recipe.User?.ExternalId);

        if (user != null)
            recipe.UserId = user.Id;

        _recipeRepository.Update(_mapper.Map<Recipe>(recipe));
        await _unitOfWork.CompleteAsync();
    }

    public async Task<Recipe> GetById(int id)
    {
        return await _recipeRepository.GetByIdAsync(id);
    }

    public async Task<ResultDto<Recipe>> GetAll(RequestDto query)
    {
        var cacheKey = GetCacheKeyForRecipeQuery(query);

        var response =
            await _cache.GetOrCreateAsync(cacheKey, (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return _recipeRepository.GetAll(query);
            });

        return response;
    }

    public async Task<Recipe> Inactivate(int id)
    {
        var recipe = await _recipeRepository.GetByIdAsync(id);
        recipe.Active = false;
        _recipeRepository.Update(recipe);
        await _unitOfWork.CompleteAsync();

        return recipe;
    }

    private string GetCacheKeyForRecipeQuery(RequestDto query, string recipeId = "")
    {
        var key = CacheKeys.RecipeList.ToString();

        if (!recipeId.IsNullOrEmpty())
            key = string.Concat(key, "_", recipeId);

        key = string.Concat(key, "_", query.Page, "_", query.PageSize);

        return key;
    }
}