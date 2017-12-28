namespace AuthenticationWebWcf.Common.Interfaces
{
    public interface IFixedToken
    {
        string GetToken();

        void SetToken(string token);
    }
}
