using MasterChef.Application.Interfaces;
using MasterChef.Domain.Entities;
using MasterChef.Infra.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MasterChef.Domain.Interface;
using MasterChef.Dto;

namespace MasterChef.Application.Services;

public class RecipeAppAppService : IRecipeAppService
{
    private readonly IIngredientAppService _ingredientAppService;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<RecipeDto> _validation;
    private readonly IEventService _eventService;
    private readonly IRecipeRepository _recipeRepository;

    public RecipeAppAppService(
        IValidator<RecipeDto> validation,
        IEventService eventService,
        IRecipeRepository recipeRepository,
        IIngredientAppService ingredientAppService,
        IUserRepository userRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        this._validation = validation;
        this._eventService = eventService;
        this._recipeRepository = recipeRepository;
        this._ingredientAppService = ingredientAppService;
        _userRepository = userRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
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

    public async Task<List<Recipe>> GetAllByUserId(string id)
    {
        return await _recipeRepository.GetAllRecipesByUserId(id);
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

    public async Task<List<Recipe>> GetAll()
    {
        var response = await _recipeRepository.GetAll();
        return response.Where(r => r.Active).ToList();
    }

    public async Task<Recipe> Inactivate(int id)
    {
        var recipe = await _recipeRepository.GetByIdAsync(id);
        recipe.Active = false;
        _recipeRepository.Update(recipe);
        await _unitOfWork.CompleteAsync();

        return recipe;
    }
}