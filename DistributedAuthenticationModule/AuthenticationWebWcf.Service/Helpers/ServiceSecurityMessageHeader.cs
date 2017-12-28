using System.ServiceModel.Channels;

namespace AuthenticationWebWcf.Service.Helpers
{
    public static class ServiceSecurityMessageHeader
    {
        public static string GetToken(this MessageHeaders headers)
        {
            var index = headers.FindHeader("Token", string.Empty);
            return index >= 0 ? headers.GetHeader<string>(index) : null;
        }

        public static void SetToken(this MessageHeaders headers, object token)
        {
            var tokenHeader = MessageHeader.CreateHeader("Token", string.Empty, token);
            headers.Add(tokenHeader);
        }
    }
}
