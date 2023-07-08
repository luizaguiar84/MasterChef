using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MasterChef.Application.Interfaces;
using MasterChef.Application.Services;
using MasterChef.Domain.Entities;
using MasterChef.Domain.Interface;
using MasterChef.Dto.Dto;
using MasterChef.Infra.AutoMapper;
using MasterChef.Infra.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace Test.Application;

public class RecipeAppServiceTests
{
    private IRecipeAppService _service;

    private readonly IMapper _mapper;
    private readonly Mock<IUserRepository> _userRepository = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly Mock<IMemoryCache> _cache = new();
    private readonly Mock<IValidator<RecipeDto>> _validation = new();
    private readonly Mock<IEventService> _eventService = new();
    private readonly Mock<IRecipeRepository> _recipeRepository = new();

    public RecipeAppServiceTests()
    {
        var myProfile = new MappingProfiles();
        var configuration = new MapperConfiguration(c => c.AddProfile(myProfile));
        _mapper = new Mapper(configuration);

        _service = new RecipeAppService(
            _validation.Object,
            _eventService.Object,
            _recipeRepository.Object,
            _userRepository.Object,
            _mapper,
            _unitOfWork.Object,
            _cache.Object
        );
    }

    [Fact]
    public async Task GetRecipeById()
    {
        var id = 1;

        var recipeResponse = new Recipe() { Id = id };

        _recipeRepository.Setup(r => r.GetByIdAsync(id))
            .ReturnsAsync(recipeResponse);

        var recipe = await _service.GetById(id);

        Assert.NotNull(recipe);
    }

    [Fact]
    public async Task GetRecipeByIdWithInvalidId()
    {
        var id = 1;

        var recipeResponse = new Recipe() { Id = id };

        _recipeRepository.Setup(r => r.GetByIdAsync(id))
            .ReturnsAsync(recipeResponse);

        var recipe = await _service.GetById(id + 1);

        Assert.Null(recipe);
    }

    [Fact]
    public async Task InactivateRecipes()
    {
        var id = 1;

        var activeRecipe = new Recipe()
        {
            Active = true,
            Id = id
        };

        _recipeRepository.Setup(r => r.GetByIdAsync(id))
            .ReturnsAsync(activeRecipe);

        var recipe = await _service.Inactivate(id);

        Assert.NotNull(recipe);
        Assert.False(recipe.Active);
    }

    [Fact]
    public async Task SaveRecipe()
    {
        var id = 1;

        var dto = new RecipeDto()
        {
            User = new UserDto()
            {
                ExternalId = "externalId"
            }
        };

        var responseRecipe = new Recipe()
        {
            Active = true,
            Id = id
        };
        var user = new User() { Id = 1 };

        _validation.Setup(v => v.ValidateAsync(dto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _recipeRepository.Setup(r => r.GetByIdAsync(id))
            .ReturnsAsync(responseRecipe);

        _userRepository.Setup(u => u.GetByExternalId(dto.User.ExternalId))
            .ReturnsAsync(user);

        _recipeRepository.Setup(r => r.AddAsync(It.IsAny<Recipe>()))
            .ReturnsAsync(responseRecipe);

        var recipe = await _service.Save(dto);

        Assert.NotNull(recipe);
    }
}