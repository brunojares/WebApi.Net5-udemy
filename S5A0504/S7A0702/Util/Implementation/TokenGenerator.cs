
using Microsoft.IdentityModel.Tokens;
using S6A0702.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace S6A0702.Util.Implementation
{
    public class TokenGenerator : ITokenGenerator
    {
        private TokenConfiguration _tokenConfiguration;

        public TokenGenerator(TokenConfiguration tokenConfiguration)
        {
            _tokenConfiguration = tokenConfiguration;
        }

        public string GetAccessToken(IEnumerable<Claim> claims)
        {
            var _secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfiguration.Secret));
            var _signinCredentias = new SigningCredentials(_secretKey, SecurityAlgorithms.HmacSha256);
            var _options = new JwtSecurityToken(
                _tokenConfiguration.Issuer
                , _tokenConfiguration.Audience
                , claims
                , DateTime.Now
                , DateTime.Now.AddMinutes(_tokenConfiguration.Minutes),
                _signinCredentias
            );
            return new JwtSecurityTokenHandler().WriteToken(_options);
        }
        public string GetRefreshToken()
        {
            using (var _random = RandomNumberGenerator.Create())
            {
                var _randomNumber = new byte[32];
                _random.GetBytes(_randomNumber);
                return Convert.ToBase64String(_randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpired(string token)
        {
            var _tokenValidationsParameters = new TokenValidationParameters()
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfiguration.Secret)),
                ValidateLifetime = false
            };
            var _handler = new JwtSecurityTokenHandler();
            var _result = _handler.ValidateToken(token, _tokenValidationsParameters, out SecurityToken securityToken);
            var _validToken = securityToken as JwtSecurityToken;
            if(
                _validToken == null || 
                !_validToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCulture)
            )
            {
                throw new SecurityTokenException("Invalid Token");
            }
            //======
            return _result;
        }

    }
}
