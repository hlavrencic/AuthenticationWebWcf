using System.Collections.Generic;
using AuthenticationWebWcf.Common.DataContracts;

namespace AuthenticationWebWcf.Service.Biz
{
    public interface IActionValidation<out T> where T : AuthenticatedDto
    {
        T Validate(string token, string tokenKey, IList<string> permisosRequeridos);
    }
}