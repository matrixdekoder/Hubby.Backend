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

        public TokenModel Create(string username)
        {
            var accessToken = GetTokenString(username, _configuration.AccessExpirationSeconds);
            var refreshToken = GetTokenString(username, _configuration.RefreshExpirationSeconds);
            return new TokenModel(accessToken, refreshToken);
        }

        public bool IsTokenExpired(string token)
        {   
            new JwtSecurityTokenHandler().ValidateToken(token, GetExpiredTokenParameters(), out var securityToken);
            IsTokenValid(securityToken);
            return securityToken.ValidTo < DateTime.Now;
        }

        public ClaimsPrincipal GetExpiredTokenClaimPrincipal(string token)
        {
            var principal = new JwtSecurityTokenHandler().ValidateToken(token, GetExpiredTokenParameters(), out var securityToken);
            IsTokenValid(securityToken);
            return principal;
        }

        private string GetTokenString(string username, int expirationSeconds)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = GetDescriptor(username, expirationSeconds);
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

        private TokenValidationParameters GetExpiredTokenParameters()
        {
            return new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret)),
                ValidateLifetime = false
            };
        }

        private static void IsTokenValid(SecurityToken securityToken)
        {
            if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
        }
    }
}