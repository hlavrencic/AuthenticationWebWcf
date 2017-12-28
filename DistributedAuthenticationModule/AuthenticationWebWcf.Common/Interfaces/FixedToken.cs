namespace AuthenticationWebWcf.Common.Interfaces
{
    public class FixedToken : IFixedToken
    {
        public static string Token { get; set; }

        public string GetToken()
        {
            return Token;
        }

        public void SetToken(string token)
        {
            Token = token;
        }
    }
}