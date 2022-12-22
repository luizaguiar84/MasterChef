using MasterChef.Domain.Entities;
using System;
using System.Threading.Tasks;
using AutoMapper;
using MasterChef.Application.Interfaces;
using MasterChef.Domain.Interface;
using MasterChef.Dto.Dto;
using MasterChef.Dto.Resources;
using MasterChef.Dto.ResponseDto;
using MasterChef.Infra.Cache;
using MasterChef.Infra.Interfaces;

namespace MasterChef.Application.Services
{
    public class IngredientAppService : IIngredientAppService
    {
        private readonly IEventService _eventService;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public IngredientAppService(
            IEventService eventService,
            IIngredientRepository ingredientRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this._eventService = eventService;
            this._ingredientRepository = ingredientRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IngredientResponseDto> AddAsync(IngredientDto ingredientDto)
        {
            var ingredient = _mapper.Map<Ingredient>(ingredientDto);

            ingredient.CreateDate = DateTime.Now;
            ingredient.LastChange = DateTime.Now;
            await _ingredientRepository.AddAsync(ingredient);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<IngredientResponseDto>(ingredient);
        }

        public async Task UpdateAsync(IngredientDto ingredientDto)
        {
            var ingredient = _mapper.Map<Ingredient>(ingredientDto);

            _ingredientRepository.Update(ingredient);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<ResultDto<IngredientResponseDto>> GetByRecipeId(RequestDto query, int recipeId)
        {
            var response = await _ingredientRepository.GetByRecipeId(query, recipeId);
            return _mapper.Map<ResultDto<IngredientResponseDto>>(response);
        }

        public async Task Delete(int id)
        {
            await _ingredientRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<ResultDto<IngredientResponseDto>> GetAll(RequestDto query)
        {
            var response = await _ingredientRepository.GetAll(query);
            return _mapper.Map<ResultDto<IngredientResponseDto>>(response);
        }

        public async Task<IngredientResponseDto> GetById(int id)
        {
            var response = await _ingredientRepository.GetByIdAsync(id);
            return _mapper.Map<IngredientResponseDto>(response);
        }

        private string GetCacheKeyForIngredientQuery(RequestDto query, int id = 0)
        {
            var key = CacheKeys.IngredientsList.ToString();

            if (id > 0)
                key = string.Concat(key, "_", id);

            key = string.Concat(key, "_", query.Page, "_", query.PageSize);

            return key;
        }
    }
}