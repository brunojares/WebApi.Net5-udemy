using S6A0702.Moldel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S6A0702.VO.v1
{
    public class LoginRefreshInVO:IVO<Token>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public void CopyFrom(Token entity)
        {
            if (entity != null)
            {
                AccessToken = entity.AccessToken;
                RefreshToken = entity.RefreshToken;
            }
        }

        public void CopyTo(ref Token entity)
        {
            if (entity != null)
            {
                entity.AccessToken = AccessToken;
                entity.RefreshToken = RefreshToken;
            }
        }
    }
}
