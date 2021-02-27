using S6A0702.Moldel.Entities;

namespace S6A0702.Business
{
    public interface ILoginBusiness
    {
        Token Autenticate(string userName, string password);
        void Authorize(string userName);
        Token Refresh(string accessToken, string refreshToken);
        void RevokeToken(string userName);
    }
}
