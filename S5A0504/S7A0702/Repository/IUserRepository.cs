using S6A0702.Moldel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S6A0702.Repository
{
    public interface IUserRepository
    {
        User GetByAuthentication(string userName, string password);
        User GetByRefreshToken(string userName, string refreshToken);
        void RefreshCredentials(ref User user);
    }
}
