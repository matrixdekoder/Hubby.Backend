using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Core.Infrastructure.Security
{
    public class TokenHandler : ITokenHandler
    {
        private readonly TokenConfiguration _configuration;

        public TokenHandler(IOptions<TokenConfiguration> options)
        {
            _configuration = options.Value;
        }

        public string Create(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = GetDescriptor(username, _configuration.Expiration);
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private SecurityTokenDescriptor GetDescriptor(string username, int expirationSeconds)
        {
            var key = Encoding.ASCII.GetBytes(_configuration.Secret);

            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.Now.AddSeconds(expirationSeconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };
        }
    }
}