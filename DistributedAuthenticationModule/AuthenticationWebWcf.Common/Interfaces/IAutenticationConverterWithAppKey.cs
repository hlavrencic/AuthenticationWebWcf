using AuthenticationWebWcf.Common.DataContracts;

namespace AuthenticationWebWcf.Common.Interfaces
{
    public interface IAutenticationConverterWithAppKey
    {
        string Encrypt(object dto);
        T Decrypt<T>(string token) where T : AuthenticatedDto;
    }
}