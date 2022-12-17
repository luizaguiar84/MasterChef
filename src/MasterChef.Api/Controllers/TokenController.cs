using MasterChef.Application.Interfaces;
using MasterChef.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MasterChef.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserAppService _userAppService;

        public TokenController(ITokenService tokenService, IUserAppService userAppService)
        {
            this._tokenService = tokenService;
            this._userAppService = userAppService;
        }
        [HttpPost]
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
            
            //userAppService.CreateNewUser(user);

            return await _userAppService.IsValidUserAndPassword(user);
        }
    }
}
