using AutoMapper;
using MasterChef.Domain.Entities;
using MasterChef.Domain.Models;
using MasterChef.Dto.Dto;
using MasterChef.Dto.ResponseDto;
using Microsoft.AspNetCore.Identity;

namespace MasterChef.Infra.AutoMapper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Recipe, RecipeDto>();
        CreateMap<RecipeDto, Recipe>();
        
        CreateMap<RecipeResponseDto, Recipe>();
        CreateMap<Recipe, RecipeResponseDto>();

        CreateMap<RecipeResponseDto, RecipeModel>();
        CreateMap<ResultDto<Recipe>, ResultDto<RecipeResponseDto>>();
        
        CreateMap<RecipeModel, RecipeDto>();
        CreateMap<RecipeDto, RecipeModel>();

        CreateMap<Ingredient, IngredientResponseDto>();
        CreateMap<IngredientDto, Ingredient>();
        
        CreateMap<IngredientResponseDto, IngredientModel>();
        CreateMap<ResultDto<IngredientResponseDto>, ResultDto<IngredientModel>>();
        CreateMap<ResultDto<IngredientModel>, ResultDto<IngredientResponseDto>>();

        CreateMap<ResultDto<Ingredient>, ResultDto<IngredientResponseDto>>();
        CreateMap<ResultDto<IngredientModel>, ResultDto<IngredientResponseDto>>();


        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();        
    }    
}