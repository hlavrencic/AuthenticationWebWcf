using AuthenticationWebWcf.Common.DataContracts;

namespace AuthenticationWebWcf.Service.Biz
{
    public interface IServiceAuthentication<out T> where T : AuthenticatedDto
    {
        T GetAuthenticatedData(string token, string tokenKey);
    }
}