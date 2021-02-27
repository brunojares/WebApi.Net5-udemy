using Microsoft.AspNetCore.Mvc.ViewFeatures;
using S6A0702.Configuration;
using S6A0702.Moldel.Entities;
using S6A0702.Moldels.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace S6A0702.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private WebApi001Context _context;
        private TokenConfiguration _tokenConfiguration;

        public UserRepository(WebApi001Context context, TokenConfiguration tokenConfiguration)
        {
            _context = context;
            _tokenConfiguration = tokenConfiguration;
        }

        public User GetByAuthentication(string userName, string password)
        {
            password = GetHashCode(password);
            return _context.Users.FirstOrDefault(item => 
                item.UserName == userName &&
                item.Password == password
            );
        }
        public User GetByRefreshToken(string userName, string refreshToken) =>
            _context.Users.FirstOrDefault(item => 
                item.UserName == userName &&
                item.RefreshToken == refreshToken
            )
        ;
        public bool VerifyIsAuthenticated(string userName) =>
            _context.Users.Count(item =>
                item.UserName == userName &&
                item.RefreshToken != null &&
                DateTime.Now.AddMinutes(_tokenConfiguration.Minutes) <= item.RefreshTokenExpiryTime
            ) > 0
        ;

        public void RefreshCredentials(ref User user)
        {
            var _dataBaseUser =
                GetById(user.Id) ??
                throw new KeyNotFoundException($"User {user.Id} not found")
            ;
            _context.Entry(_dataBaseUser).CurrentValues.SetValues(user);
            _context.SaveChanges();
        }
        public void RevokeToken(string userName)
        {
            var _user =
                _context.Users.FirstOrDefault(item => item.UserName == userName) ??
                throw new KeyNotFoundException($"User {userName} not found")
            ;
            _user.RefreshToken = null;
            _user.RefreshTokenExpiryTime = DateTime.Now;
            _context.SaveChanges();
        }

        private User GetById(long id) =>
            _context.Users.FirstOrDefault(item => item.Id == id)
        ;
        private string GetHashCode(string password)
        {
            var _inputBytes = Encoding.UTF8.GetBytes(password);
            var _provider = new SHA256CryptoServiceProvider();
            var _hashedBytes = _provider.ComputeHash(_inputBytes);
            return BitConverter.ToString(_hashedBytes);
        }
    }
}