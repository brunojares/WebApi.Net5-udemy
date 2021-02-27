using Microsoft.IdentityModel.JsonWebTokens;
using S6A0702.Configuration;
using S6A0702.Moldel.Entities;
using S6A0702.Repository;
using S6A0702.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Threading.Tasks;

namespace S6A0702.Business.Implementation
{
    public class LoginBusiness : ILoginBusiness
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";

        private TokenConfiguration _tokenConfiguration;
        private IUserRepository _userRepository;
        private ITokenGenerator _tokenGenerator;

        public LoginBusiness(TokenConfiguration tokenConfiguration, IUserRepository userRepository, ITokenGenerator tokenGenerator)
        {
            _tokenConfiguration = tokenConfiguration;
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
        }

        public Token Autenticate(string userName, string password)
        {
            var _user =
                _userRepository.GetByAuthentication(userName, password) ??
                throw new SecurityException($"Username or password invalid")
            ;
            var _claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, _user.UserName)
            };
            var _accessToken = _tokenGenerator.GetAccessToken(_claims);
            var _refreshToken = _tokenGenerator.GetRefreshToken();
            _user.RefreshToken = _refreshToken;
            _user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_tokenConfiguration.Days);
            var _createDate = DateTime.Now;
            var _exprationDate = _createDate.AddMinutes(_tokenConfiguration.Minutes);

            _userRepository.RefreshCredentials(ref _user);

            return new Token(
                true,
                _createDate.ToString(DATE_FORMAT),
                _exprationDate.ToString(DATE_FORMAT),
                _accessToken,
                _refreshToken
            );
        }

        public void Authorize(string userName)
        {
            if (!_userRepository.VerifyIsAuthenticated(userName))
                throw new SecurityException($"User {userName} is not authenticated");
        }

        public Token Refresh(string accessToken, string refreshToken)
        {
            var _principal = _tokenGenerator.GetPrincipalFromExpired(accessToken);
            var _userName = _principal.Identity.Name;
            var _user = 
                _userRepository.GetByRefreshToken(_userName, refreshToken) ??
                throw new SecurityException($"User {_userName} or Refresh Token not found")
            ;
            if (_user.RefreshTokenExpiryTime < DateTime.Now)
                throw new SecurityException("Token has expired");
            //======
            accessToken = _tokenGenerator.GetAccessToken(_principal.Claims);
            _user.RefreshToken = _tokenGenerator.GetRefreshToken();
            var _createDate = DateTime.Now;
            _user.RefreshTokenExpiryTime = _createDate.AddMinutes(_tokenConfiguration.Minutes);
            _userRepository.RefreshCredentials(ref _user);
            //======
            return new Token(
                true,
                _createDate.ToString(DATE_FORMAT),
                _user.RefreshTokenExpiryTime.ToString(DATE_FORMAT),
                accessToken,
                _user.RefreshToken
            );
        }
        public void RevokeToken(string userName) =>
            _userRepository.RevokeToken(userName)
        ;
    }
}
