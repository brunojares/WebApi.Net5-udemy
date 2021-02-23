using S6A0702.Moldel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S6A0702.VO.v1
{
    public class LoginOutVO : IVO<Token>
    {
        public bool Autenticated { get; set; }
        public string Created { get; set; }
        public string Expiration { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public void CopyFrom(Token entity)
        {
            if (entity != null)
            {
                Autenticated = entity.Autenticated;
                Created = entity.Created;
                Expiration = entity.Expiration;
                AccessToken = entity.AccessToken;
                RefreshToken = entity.RefreshToken;
            }
        }

        public void CopyTo(ref Token entity)
        {
            if (entity != null)
            {
                entity.Autenticated = Autenticated;
                entity.Created = Created;
                entity.Expiration = Expiration;
                entity.AccessToken = AccessToken;
                entity.RefreshToken = RefreshToken;
            }
        }
    }
}
