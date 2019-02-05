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

        public string Handle(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var descriptor = GetDescriptor(username);
            var securityToken = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(securityToken);
        }

        private SecurityTokenDescriptor GetDescriptor(string username)
        {
            var key = Encoding.ASCII.GetBytes(_configuration.Secret);

            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.Now.AddSeconds(_configuration.ExpirationSeconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
        }
    }
}