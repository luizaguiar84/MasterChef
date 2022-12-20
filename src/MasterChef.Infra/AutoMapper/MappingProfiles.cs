using AutoMapper;
using MasterChef.Domain.Entities;
using MasterChef.Domain.Models;
using MasterChef.Dto;
using Microsoft.AspNetCore.Identity;

namespace MasterChef.Infra.AutoMapper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Recipe, RecipeDto>();
        CreateMap<RecipeDto, Recipe>();
        
        CreateMap<RecipeModel, RecipeDto>();
        CreateMap<RecipeDto, RecipeModel>();

        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();        
    }    
}