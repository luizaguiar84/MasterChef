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
        private readonly ITokenService tokenService;
        private readonly IUserService userService;

        public TokenController(ITokenService tokenService, IUserService userService)
        {
            this.tokenService = tokenService;
            this.userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> Create(TokenInfo model)
        {
            if (await IsValidUserAndPassword(model.UserName, model.Password))
            {
                var token = tokenService.GenerateToken(model.UserName);

                return new OkObjectResult(new { token = token });
            }

            return Unauthorized();
        }

        private async Task<bool> IsValidUserAndPassword(string userName, string password)
        {
            var user = new User()
            {
                UserName = userName,
                Password = password
            };
            
            //userService.CreateNewUser(user);

            return await userService.IsValidUserAndPassword(user);
        }
    }
}
