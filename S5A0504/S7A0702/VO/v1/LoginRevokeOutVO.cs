using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S6A0702.VO.v1
{
    public class LoginRevokeOutVO
    {
        public string userName { get; set; }
        public LoginRevokeOutVO(string userName)
        {
            this.userName = userName;
        }
    }
}
