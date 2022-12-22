using MasterChef.Application.Interfaces;
using MasterChef.Domain.Interface;
using MasterChef.Domain.Resources;
using MasterChef.Dto.Dto;
using MasterChef.Dto.Resources;
using MasterChef.Dto.ResponseDto;
using MasterChef.Infra.Helpers.ExtensionMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MasterChef.Api.Controllers
{
    /// <summary>
    /// Controller to add / edit recipe ingredients
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors("Default")]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientAppService _ingredientAppService;
        private readonly IEventService _eventService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ingredientAppService"></param>
        /// <param name="eventService"></param>
        public IngredientController(
            IIngredientAppService ingredientAppService,
            IEventService eventService)
        {
            _ingredientAppService = ingredientAppService;
            _eventService = eventService;
        }
        
        /// <summary>
        /// Get all ingredients
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultDto<IngredientResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] RequestDto query)
        {
            var response = await _ingredientAppService.GetAll(query);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        /// <summary>
        /// Get ingredient by Id
        /// </summary>
        /// <param name="id">recipe id</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(IngredientResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _ingredientAppService.GetById(id);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        /// <summary>
        /// Get all ingredients from the recipe
        /// </summary>
        /// <param name="query"></param>
        /// <param name="id">recipe id</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultDto<IngredientResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("getbyrecipeId/{id}")]
        public async Task<IActionResult> Get([FromQuery] RequestDto query, int id)
        {
            var response = await _ingredientAppService.GetByRecipeId(query, id);

            if (response == null)
                return NotFound();

            return Ok(response);
        }
        

        /// <summary>
        /// Add a ingredient
        /// </summary>
        /// <param name="ingredient"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(IngredientResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Post(IngredientDto ingredient)
        {
            var response = await _ingredientAppService.AddAsync(ingredient);

            if (_eventService.Event.EventsList.HasItems())
                return BadRequest(new ErrorResource(_eventService.Event.EventsList));

            return CreatedAtAction(nameof(Get), new { id = ingredient.Id }, response);
        }


        /// <summary>
        /// update a recipe
        /// </summary>
        /// <param name="ingredient"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(IngredientDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task<IActionResult> Put(IngredientDto ingredient)
        {
            await _ingredientAppService.UpdateAsync(ingredient);

            if (_eventService.Event.EventsList.HasItems())
                return BadRequest(new ErrorResource(_eventService.Event.EventsList));

            return Ok(ingredient);
        }

        /// <summary>
        /// delete a ingredient
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status500InternalServerError)]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _ingredientAppService.Delete(id);

            if (_eventService.Event.EventsList.HasItems())
                return BadRequest(new ErrorResource(_eventService.Event.EventsList));

            return NoContent();
        }
    }
}