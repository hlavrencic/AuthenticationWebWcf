using AuthenticationWebWcf.Common.DataContracts;

namespace AuthenticationWebWcf.Common.Interfaces
{
    public interface IAutenticationConverter
    {
        string Encrypt(object dto, string key);

        T Decrypt<T>(string token, string key) where T : AuthenticatedDto;
    }
}
