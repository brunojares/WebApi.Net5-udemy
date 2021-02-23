using Microsoft.AspNetCore.Mvc.ViewFeatures;
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
        protected WebApi001Context Context { get; }
        public UserRepository(WebApi001Context context)
        {
            Context = context;
        }

        public User GetByAuthentication(string userName, string password)
        {
            password = GetHashCode(password);
            return Context.Users.FirstOrDefault(item => 
                item.UserName == userName &&
                item.Password == password
            );
        }
        public void RefreshCredentials(ref User user)
        {
            var _dataBaseUser =
                GetById(user.Id) ??
                throw new KeyNotFoundException($"User {user.Id} not found")
            ;
            Context.Entry(_dataBaseUser).CurrentValues.SetValues(user);
            Context.SaveChanges();
        }

        private User GetById(long id) =>
            Context.Users.FirstOrDefault(item => item.Id == id)
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