using MasterChef.Application.Interfaces;
using MasterChef.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace MasterChef.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserAppService _userService;
        private readonly ILogger _logger;

        public UserController(
            IUserAppService userService,
            ILogger logger
            )
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post(
            [FromBody] User user
            )
        {
            if (await _userService.IsValidUserAndPassword(user))
                return Ok();

            return Unauthorized();

        }

        [HttpPost]
        [Route("createUser")]
        public async Task<IActionResult> CreateUser(
            [FromBody] User user
        )
        {
            try
            {
                await _userService.CreateNewUser(user);
                return Created("", user);

            }
            catch (Exception e)
            {
                _logger.Error(e, "");
                return BadRequest();
            }



        }
    }
}
