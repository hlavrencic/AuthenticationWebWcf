using System.Collections.Generic;
using System.Web;
using AuthenticationWebWcf.Common.DataContracts;

namespace AuthenticationWebWcf.Web.Business
{
    public interface IAuthenticatedUserManager<T> where T : AuthenticatedDto
    {
        KeyValuePair<string, T> ReadUser(HttpRequestBase request);
    }
}