using System.Collections.Generic;
using AuthenticationWebWcf.Common.DataContracts;

namespace AuthenticationWebWcf.Web.Business
{
    public interface IAuthenticatedUserReader<T> where T : AuthenticatedDto
    {
        KeyValuePair<string, T> GetUser();

        KeyValuePair<string, T> GetUser(string token);
    }
}