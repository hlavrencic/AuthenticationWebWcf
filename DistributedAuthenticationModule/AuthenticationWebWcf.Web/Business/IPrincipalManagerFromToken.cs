using System.Security.Principal;
using System.Web;

namespace AuthenticationWebWcf.Web.Business
{
    public interface IPrincipalManagerFromToken
    {
        IPrincipal GetPrincipal(HttpRequestBase request);
    }
}
