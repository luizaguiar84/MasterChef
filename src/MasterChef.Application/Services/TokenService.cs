using MasterChef.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MasterChef.Application.Services
{
    public class TokenService : ITokenService
    {
        public string GenerateToken(string userName)
        {
            var claims = new Claim[]
               {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                    new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
               };

            var symmetricSecurityKey = new SymmetricSecurityKey(Security.GetKey());
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtHeader = new JwtHeader(signingCredentials);
            var jwtPayload = new JwtPayload(claims);

            var token = new JwtSecurityToken(jwtHeader, jwtPayload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
