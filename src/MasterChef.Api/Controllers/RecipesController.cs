using MasterChef.Application.Interfaces;
using MasterChef.Domain.Entities;
using MasterChef.Domain.Interface;
using MasterChef.Domain.Resources;
using MasterChef.Dto;
using MasterChef.Infra.Helpers.ExtensionMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace MasterChef.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RecipesController : ControllerBase
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
        public RecipesController(
            ILogger logger,
            IRecipeAppService recipeAppService,
            IEventService eventService)
        {
            _logger = logger;
            _recipeAppService = recipeAppService;
            _eventService = eventService;
        }

        /// <summary>
        /// Get All Recipes
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<Recipe>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Recipe>), StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var response = await _recipeAppService.GetAll();
                return Ok(response);

            }
            catch (Exception e)
            {

                throw;
            }            
        }

        /// <summary>
        /// Get recipe by User id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<Recipe>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Recipe>), StatusCodes.Status404NotFound)]
        [Route("getRecipeByUser/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetRecipeByUser(string id)
        {
            var response = new List<Recipe>();
            
            var recipes = await _recipeAppService.GetAllByUserId(id);

            if (recipes!= null) 
                response = recipes;

            _logger.Information("Recipe : {@response}", response);
            return Ok(response);

        }

        /// <summary>
        /// Get a recipe
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(Recipe), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Recipe>), StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(typeof(RecipeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Post(RecipeDto recipe)
        {
            var response = await _recipeAppService.Save(recipe);

            if (_eventService.Event.EventsList.HasItems())
                 return BadRequest( new ErrorResource(_eventService.Event.EventsList));

            _logger.Information("Recipe : {@response}", response);
            return CreatedAtAction(nameof(Get), new {id = response.Id }, response);
        }

        /// <summary>
        /// Update a recipe
        /// </summary>
        /// <param name="recipe">recipe id</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(RecipeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status400BadRequest)]
        [HttpPut]
        public async Task<IActionResult> Put(RecipeDto recipe)
        {
            var response = await _recipeAppService.Update(recipe);

            if (_eventService.Event.EventsList.HasItems())
                return BadRequest(new ErrorResource(_eventService.Event.EventsList));

            _logger.Information("Recipe : {@response}", response);
            return Ok(response);

        }

        /// <summary>
        /// Inactivate a recipe
        /// </summary>
        /// <param name="id">recipe id</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(Recipe), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status400BadRequest)]
        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _recipeAppService.Inactivate(id);

            if (_eventService.Event.EventsList.HasItems())
                return BadRequest(new ErrorResource(_eventService.Event.EventsList));
            
            _logger.Information("Inactivate Recipe : {@response}", response);

            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }
    }
}
