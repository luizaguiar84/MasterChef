using MasterChef.Application.Interfaces;
using MasterChef.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MasterChef.Api.Controllers
{
    /// <summary>
    /// Token
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserAppService _userAppService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenService"></param>
        /// <param name="userAppService"></param>
        public TokenController(ITokenService tokenService, IUserAppService userAppService)
        {
            this._tokenService = tokenService;
            this._userAppService = userAppService;
        }
        
        /// <summary>
        /// Get a token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(TokenInfo model)
        {
            if (await IsValidUserAndPassword(model.Username, model.Password))
            {
                var token = _tokenService.GenerateToken(model.Username);

                return new OkObjectResult(new { token = token });
            }

            return Unauthorized();
        }

        private async Task<bool> IsValidUserAndPassword(string userName, string password)
        {
            var user = new User()
            {
                Username = userName,
                Password = password
            };
            
            return await _userAppService.IsValidUserAndPassword(user);
        }
    }
}
