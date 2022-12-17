using MasterChef.Application.Interfaces;
using MasterChef.Domain.Entities;
using MasterChef.Domain.Interface;
using MasterChef.Infra.Helpers.ExtensionMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MasterChef.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeAppService _recipeAppService;
        private readonly IEventService _eventService;
        public RecipesController(
            IRecipeAppService recipeAppService,
            IEventService eventService)
        {
            this._recipeAppService = recipeAppService;
            this._eventService = eventService;
        }

        [ProducesResponseType(typeof(Recipe), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _recipeAppService.GetAll();
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

            return Ok(response);

        }


        [ProducesResponseType(typeof(Recipe), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Post(Recipe recipe)
        {
            var response = await _recipeAppService.Save(recipe);
            
            if (_eventService.Event.EventsList.HasItems())
                return BadRequest(_eventService.Event.EventsList);

            return Ok(response);
        }

        [ProducesResponseType(typeof(Recipe), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task<IActionResult> Put(Recipe recipe)
        {
            var response = await _recipeAppService.Update(recipe);
            
            if (_eventService.Event.EventsList.HasItems())
                return BadRequest(_eventService.Event.EventsList);

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

            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }
    }
}
