using AuthenticationWebWcf.Common.DataContracts;

namespace AuthenticationWebWcf.Web.Business
{
    public interface IAutenticationConverterWithAppKey
    {
        string Encrypt(object dto);
        T Decrypt<T>(string token) where T : AuthenticatedDto;
    }
}