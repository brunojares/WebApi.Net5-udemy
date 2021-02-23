using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace S6A0702.Util
{
    public interface ITokenGenerator
    {
        string Generate(IEnumerable<Claim> claims);
        string Refresh();
        ClaimsPrincipal GetPrincipalFromExpired(string token);
    }
}
