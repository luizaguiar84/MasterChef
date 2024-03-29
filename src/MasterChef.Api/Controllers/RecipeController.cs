﻿using MasterChef.Application.Interfaces;
using MasterChef.Domain.Entities;
using MasterChef.Domain.Interface;
using MasterChef.Domain.Resources;
using MasterChef.Dto.Dto;
using MasterChef.Dto.Resources;
using MasterChef.Dto.ResponseDto;
using MasterChef.Infra.Helpers.ExtensionMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace MasterChef.Api.Controllers
{
    /// <summary>
    /// Recipe Controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    [EnableCors("Default")]
    public class RecipeController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IRecipeAppService _recipeAppService;
        private readonly IEventService _eventService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="recipeAppService"></param>
        /// <param name="eventService"></param>
        public RecipeController(
            ILogger logger,
            IRecipeAppService recipeAppService,
            IEventService eventService)
        {
            _logger = logger;
            _recipeAppService = recipeAppService;
            _eventService = eventService;
        }

        /// <summary>
        /// Get All Recipe
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultDto<RecipeResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultDto<RecipeResponseDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] RecipeRequestDto query)
        {
            var response = await _recipeAppService.GetAll(query);
            return Ok(response);
        }

        /// <summary>
        /// Get recipe by User id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(RecipeResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ErrorResource>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("getRecipeByUser/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetRecipeByUser(string id, [FromQuery] RecipeRequestDto query)
        {
            var response = new ResultDto<RecipeResponseDto>();

            var recipes = await _recipeAppService.GetAllByUserId(query, id);

            if (recipes != null)
                response = recipes;

            _logger.Information("Recipe : {@response}", response);
            return Ok(response);
        }

        /// <summary>
        /// Get a recipe by Id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(RecipeResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ErrorResource>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _recipeAppService.GetById(id);

            if (response == null)
                return NotFound();

            _logger.Information("Recipe : {@response}", response);
            return Ok(response);
        }

        /// <summary>
        /// Create a recipe
        /// </summary>
        /// <param name="recipe">Recipe model</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(RecipeResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Post(RecipeDto recipe)
        {
            var response = await _recipeAppService.Save(recipe);

            if (_eventService.Event.EventsList.HasItems())
                return BadRequest(new ErrorResource(_eventService.Event.EventsList));

            _logger.Information("Recipe : {@response}", response);
            return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
        }

        /// <summary>
        /// Update a recipe
        /// </summary>
        /// <param name="recipe">recipe id</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(RecipeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task<IActionResult> Put(RecipeDto recipe)
        {
            await _recipeAppService.Update(recipe);

            if (_eventService.Event.EventsList.HasItems())
                return BadRequest(new ErrorResource(_eventService.Event.EventsList));

            return Ok(recipe);
        }

        /// <summary>
        /// Inactivate a recipe
        /// </summary>
        /// <param name="id">recipe id</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _recipeAppService.Inactivate(id);

            if (_eventService.Event.EventsList.HasItems())
                return BadRequest(new ErrorResource(_eventService.Event.EventsList));

            _logger.Information("Inactivate Recipe : {@response}", response);

            return NoContent();
        }
    }
}