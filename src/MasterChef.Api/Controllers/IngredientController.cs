using MasterChef.Application.Interfaces;
using MasterChef.Domain.Entities;
using MasterChef.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MasterChef.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientAppService _ingredientAppService;
        private readonly IEventService _eventService;

        public IngredientController(
            IIngredientAppService ingredientAppService,
            IEventService eventService)
        {
            _ingredientAppService = ingredientAppService;
            _eventService = eventService;
        }


        [ProducesResponseType(typeof(Ingredient), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var response = await _ingredientAppService.GetAll();
            
            if (response == null)
                return NotFound("Nenhum item encontrado");

            return Ok(response);

        }

        [ProducesResponseType(typeof(Ingredient), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var response = await _ingredientAppService.GetByRecipeId(id);
            
            if (response == null)
                return NotFound("Nenhum item encontrado");

            return Ok(response);

        }


        [ProducesResponseType(typeof(Ingredient), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Post(Ingredient Ingredient)
        {

            var response = await _ingredientAppService.Save(Ingredient);
            
            if (_eventService.Event.EventsList.Any())
                return BadRequest(_eventService.Event.EventsList);

            return Ok(response);

        }


        [ProducesResponseType(typeof(Ingredient), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task<IActionResult> Put(Ingredient receita)
        {

            var dados = await _ingredientAppService.Update(receita);

            if (_eventService.Event.EventsList.Any())
                return BadRequest(_eventService.Event.EventsList);

            return Ok(dados);

        }


        [ProducesResponseType(typeof(Ingredient), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status500InternalServerError)]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var response = await _ingredientAppService.Delete(id);

            if (_eventService.Event.EventsList.Any())
                return BadRequest(_eventService.Event.EventsList);

            return Ok(response);

        }

    }
}
