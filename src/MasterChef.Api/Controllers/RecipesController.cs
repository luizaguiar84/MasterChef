using MasterChef.Application.Interfaces;
using MasterChef.Domain.Entities;
using MasterChef.Domain.Interface;
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
        public RecipesController(
            ILogger logger,
            IRecipeAppService recipeAppService,
            IEventService eventService)
        {
            _logger = logger;
            _recipeAppService = recipeAppService;
            _eventService = eventService;
        }

        [ProducesResponseType(typeof(Recipe), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status404NotFound)]
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

        [ProducesResponseType(typeof(Recipe), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status404NotFound)]
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

        [ProducesResponseType(typeof(Recipe), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status404NotFound)]
        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _recipeAppService.GetById(id);

            if (response == null)
                return NotFound("Nenhum item encontrado");

            _logger.Information("Recipe : {@response}", response);
            return Ok(response);

        }


        [ProducesResponseType(typeof(RecipeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Post(RecipeDto recipe)
        {
            var response = await _recipeAppService.Save(recipe);

            if (_eventService.Event.EventsList.HasItems())
                return BadRequest(_eventService.Event.EventsList);

            _logger.Information("Recipe : {@response}", response);
            return Ok(response);
        }

        [ProducesResponseType(typeof(RecipeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task<IActionResult> Put(RecipeDto recipe)
        {
            var response = await _recipeAppService.Update(recipe);

            if (_eventService.Event.EventsList.HasItems())
                return BadRequest(_eventService.Event.EventsList);

            _logger.Information("Recipe : {@response}", response);
            return Ok(response);

        }

        [ProducesResponseType(typeof(Recipe), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status500InternalServerError)]
        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _recipeAppService.Inactivate(id);

            if (_eventService.Event.EventsList.HasItems())
                return BadRequest(_eventService.Event.EventsList);
            _logger.Information("Inactivate Recipe : {@response}", response);

            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }
    }
}
