using AuthenticationWebWcf.WebUsageExample.Model;

namespace AuthenticationWebWcf.WebUsageExample.External
{
    public interface IAuthenticationService
    {
        string Auth(UserLoginModel login);
    }
}
