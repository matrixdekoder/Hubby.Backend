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

        public TokenModel Handle(string username)
        {
            var accessToken = GetTokenString(username, _configuration.AccessExpirationSeconds);
            var refreshToken = GetTokenString(username, _configuration.RefreshExpirationSeconds);
            return new TokenModel(accessToken, refreshToken);
        }

        public bool IsTokenExpired(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret)),
                ValidateLifetime = false
            };
            
            var tokenHandler = new JwtSecurityTokenHandler();
            
            tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
            if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            
            return securityToken.ValidTo < DateTime.Now;
        }

        public ClaimsPrincipal GetExpiredTokenClaimPrincipal(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret)),
                ValidateLifetime = false
            };
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
            
            if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            
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
    }
}